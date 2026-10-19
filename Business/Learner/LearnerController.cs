using FuturePathGroup10.Business.Prerequisite;
using FuturePathGroup10.Data;
using FuturePathGroup10.Funding;
using FuturePathGroup10.Interest;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static FuturePathGroup10.Data.DB;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FuturePathGroup10.Learner
{
    public partial class LearnerController
    {
        #region Data Members
        public readonly LearnerDB learnerDB;
        public readonly SubjectDB subjectDB;
        public readonly FacultyDB facultyDB;
        public readonly DegreeDB degreeDB;

        public Faculty? SelectedFaculty { get; private set; }

        public Degree? SelectedDegree { get; private set; }

        public Learner? activeLearner { get; private set; }

        public LearnerInterestResponseDB lirDB;
        public InterestQuestionnaireDB questionnaireDB;
        public InterestCategoryDB categoryDB;

        private FundingOptionDB? fundingDB;


        // The learner's current sitting of the questionnaire (null until started)
        public LearnerInterestResponse? activeResponse { get; private set; }
        #endregion

        #region Constructor
        public LearnerController()
        {
            this.learnerDB = new LearnerDB();
            this.subjectDB = new SubjectDB(learnerDB);
            this.facultyDB = new FacultyDB(learnerDB);
            this.degreeDB = new DegreeDB(facultyDB);
            this.activeLearner = null;
        }
        #endregion

        #region Loading the interest data
        // Builds the data-class chain once, the first time the interest side is used,
        // so creating a profile never triggers it.
        // Order matters: each class needs the previous one.
        private void loadInterestData()
        {
            if (categoryDB != null) return;
            lirDB = new LearnerInterestResponseDB(learnerDB);
            questionnaireDB = new InterestQuestionnaireDB(lirDB);
            categoryDB = new InterestCategoryDB(questionnaireDB, degreeDB);
        }
        #endregion

        #region Eligibility data members
        private ProgrammeRequirementDB? requirementDB;
        private Dictionary<string, double> currentMarks =
            new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        public EligibilityResult lastResult { get; private set; }
        #endregion

        #region Create and Update learner profile
        //this should be called after register

        public bool createProfile(string username, string phoneNumber, string fullname, string school,
                          string grade11, string grade12, string email, string province, out string error)
        {
            error = "";

            if (string.IsNullOrWhiteSpace(username))
            {
                error = "A username is required (it links the profile to the login account).";
                return false;
            }

            Learner learner;
            try
            {
                learner = new Learner(learnerDB.getNextID(), username.Trim(), Clean(phoneNumber), Clean(fullname), Clean(school),
                                      Clean(grade11), Clean(grade12), Clean(email), Clean(province));
            }
            catch (Exception ex)
            {
                error = "The profile details are not valid: " + ex.Message;
                return false;
            }

            return createProfile(learner, out error);
        }

        private static string Clean(string text)
        {
            if (text == null) return "";
            return text.Trim();
        }
        public bool createProfile(Learner learner, out string error)
        {
            error = Validate(learner);
            if (error != "") return false;

            try
            {
                // assigning the next free ID
                int nextId = learnerDB.getNextID();
                learner.learnerID = nextId;

                learnerDB.DataSetChange(learner, DB.DBOperation.Add);
                if (!learnerDB.UpdateDataSource(DB.DBOperation.Add))
                {
                    try
                    {
                        learnerDB.DataSetChange(learner, DB.DBOperation.Delete);
                        learnerDB.UpdateDataSource(DB.DBOperation.Delete);
                    }
                    catch
                    {
                        learnerDB.learners.Remove(learner);
                    }

                    error = "Could not save the profile.";
                    return false;
                }

                activeLearner = learner;
                return true;
            }
            catch (Exception ex)
            {
                error = "Could not create profile: " + ex.Message;
                return false;
            }
        }

        public bool loadLearner(string? username)
        {
            activeLearner = null;
            if (username == null) return true;

            foreach (Learner existing in learnerDB.learners)
            {
                if (string.Equals(existing.Username, username))
                {
                    activeLearner = existing;
                    return true;
                }
            }
            return false;
        }

        public bool updateProfile(Learner learner, out string error)
        {
            error = Validate(learner);
            if (error != "") return false;

            if (learner.learnerID <= 0)
            {
                error = "This learner has not been created yet.";
                return false;
            }

            try
            {
                learnerDB.DataSetChange(learner, DB.DBOperation.Edit);
                if (!learnerDB.UpdateDataSource(DB.DBOperation.Edit))
                {
                    error = "Could not save the changes.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = "Could not save: " + ex.Message;
                return false;
            }

            activeLearner = learner;
            return true;
        }

        // Helper method to validate if the mandotory fileds have information profile creation and update
        // Should add more if not all idk :(
        private string Validate(Learner l)
        {
            if (string.IsNullOrWhiteSpace(l.school)) return "School is required.";
            if (string.IsNullOrWhiteSpace(l.province)) return "Province is required.";
            return "";   // empty string means valid
        }


        #endregion

        #region Selection
        public void clearSelection()
        {
            SelectedFaculty = null;
            SelectedDegree = null;
        }

        public bool selectFaculty(Reccomendation.Faculty faculty, out string error)
        {
            error = "";

            if (faculty == null)
            {
                error = "Please choose a valid faculty.";
                return false;
            }

            SelectedFaculty = faculty;
            return true;
        }

        public bool selectDegree(Degree degree, out string error)
        {
            error = "";

            if (degree == null)
            {
                error = "Please choose a valid degree degree.";
                return false;
            }

            SelectedDegree = degree;
            return true;
        }
        #endregion

        #region Marks
        public bool captureMarks(Dictionary<Subject, double> marks, out string error)
        {
            error = "";

            if (activeLearner == null)
            {
                error = "No learner was found for the logged in account, so marks cannot be saved.";
                return false;
            }

            if (marks == null || marks.Count == 0)
            {
                error = "Enter at least one subject and mark.";
                return false;
            }
            // add them all, then write once
            try
            {
                foreach (LearnerSubject previous in activeLearner.learnerSubjects.ToList())
                {
                    if (!marks.ContainsKey(previous.subject))
                    {
                        subjectDB.DataSetChange(previous, DB.DBOperation.Delete);
                    }
                }

                foreach (KeyValuePair<Subject, double> entry in marks)
                {
                    LearnerSubject eachSubject = new LearnerSubject(activeLearner, entry.Key, entry.Value);
                    subjectDB.DataSetChange(eachSubject, DB.DBOperation.Add);
                }

                if (!subjectDB.UpdateDataSource(DB.DBOperation.Add))
                {
                    error = "Could not save the marks.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = "Could not save the marks: " + ex.Message;
                return false;
            }

            return true;
        }
        #endregion

        #region Questionnare
        public Collection<InterestQuestion> getQuestions()
        {
            loadInterestData();
            return questionnaireDB.questions;
        }

        // Start the learner's sitting: creates the LearnerInterestResponse row that answers belong to
        public bool startQuestionnaire(out string error)
        {
            error = "";

            if (activeLearner == null)
            {
                error = "No learner was found for the logged in account, so marks cannot be saved.";
                return false;
            }

            try
            {
                loadInterestData();

                int nextId = 1;
                foreach (LearnerInterestResponse r in lirDB.LIRs)
                {
                    if (r.responseID >= nextId)
                    {
                        nextId = r.responseID + 1;
                    }
                }

                LearnerInterestResponse response = new LearnerInterestResponse(nextId, activeLearner, "");

                lirDB.DataSetChange(response, DB.DBOperation.Add);
                if (!lirDB.UpdateDataSource(DB.DBOperation.Add))
                {
                    error = "Could not start the questionnaire.";
                    return false;
                }

                activeLearner.addLIR(response);
                activeResponse = response;
            }
            catch (Exception ex)
            {
                error = "Could not start the questionnaire: " + ex.Message;
                return false;
            }

            return true;
        }

        public bool saveAnswer(int questionId, int answer, out string error)
        {
            error = "";

            if (activeResponse == null)
            {
                error = "Start the questionnaire first.";
                return false;
            }

            if (answer < 1 || answer > 5)
            {
                error = "Answer must be between 1 and 5";
                return false;
            }

            InterestQuestion question = questionnaireDB.getQuestion(questionId);
            if (question == null)
            {
                error = "That question does not exist.";
                return false;
            }

            if (getAnswers().ContainsKey(questionId))
            {
                error = "You have already answered this question.";
                return false;
            }

            try
            {
                InterestQuestionnaire questionnaire = new InterestQuestionnaire(activeResponse, answer.ToString());
                questionnaire.addQuestion(question);

                questionnaireDB.DataSetChange(questionnaire, question, DB.DBOperation.Add);   // in memory
                if (!questionnaireDB.UpdateDataSource(DB.DBOperation.Add))                    // database
                {
                    error = "Could not save the answer.";
                    return false;
                }
            }
            catch (Exception ex)
            {
                error = "Could not save the answer: " + ex.Message;
                return false;
            }

            return true;
        }

        //  Score per interest category (percentage, 0 to 100), keyed by InterestCategoryID
        public Dictionary<int, double> calculateInterestScores()
        {
            Dictionary<int, int> answers = getAnswers();

            if (answers.Count == 0)
            {
                return new Dictionary<int, double>();
            }

            return new Interest.InterestScorer().CalculateCategoryScores(questionnaireDB.questions, answers);
        }

        // Reads this learner's current answers out of the in-memory collection: questionID -> answer
        private Dictionary<int, int> getAnswers()
        {
            Dictionary<int, int> answers = new Dictionary<int, int>();

            if (activeResponse == null || questionnaireDB == null)
            {
                return answers;
            }

            foreach (InterestQuestionnaire iq in questionnaireDB.questionnaires)
            {
                if (iq.LIR == null || iq.LIR.responseID != activeResponse.responseID)
                {
                    continue;                                  // belongs to another sitting
                }

                foreach (InterestQuestion q in iq.questions)
                {
                    int value;
                    if (int.TryParse(iq.response, out value))  // the answer is stored as text
                    {
                        answers[q.questionID] = value;
                    }
                }
            }

            return answers;
        }

        // Interest Match (0 to 100) for the selected degree
        // Needs: the learner has answered the questionnaire, and a degree is selected.
        public double getInterestMatch(out string error)
        {
            error = "";

            if (SelectedDegree == null)
            {
                error = "Select a degree first.";
                return 0;
            }

            Dictionary<int, double> scores = calculateInterestScores();
            if (scores.Count == 0)
            {
                error = "Answer the questionnaire first.";
                return 0;
            }

            List<int> categoryIds = new List<int>();
            foreach (InterestCategory c in SelectedDegree.categories)
            {
                categoryIds.Add(c.interestCategoryID);
            }

            if (categoryIds.Count == 0)
            {
                error = "This degree has no interest categories linked to it.";
                return 0;
            }

            return new Interest.InterestScorer().CalculateDegreeMatch(scores, categoryIds);
        }

        #endregion

        #region Funding
        public Collection<FundingOption> viewFundingOptions()
        {
            fundingDB ??= new FundingOptionDB(facultyDB, degreeDB);

            return fundingDB.options;
        }
        #endregion

        #region Eligibility
        public EligibilityResult checkEligibility(Dictionary<string, double> marks, double? interestMatch)
        {
            if (SelectedDegree == null)
            {
                return new EligibilityChecker().Check("", marks, null, interestMatch);   // gives More Information Needed
            }
            return new EligibilityChecker().Check(SelectedDegree.title, marks,
                                                  SelectedDegree.requirements, interestMatch);
        }
        public bool setMarks(Dictionary<Subject, double> marks, out string error)
        {
            error = "";

            if (marks == null || marks.Count == 0)
            {
                error = "Enter at least one subject and mark.";
                return false;
            }


            foreach (KeyValuePair<Subject, double> entry in marks)
            {
                if (entry.Value < 0 || entry.Value > 100)
                {
                    error = "Marks must be between 0 and 100.";
                    return false;
                }
            }

            currentMarks.Clear();
            foreach (KeyValuePair<Subject, double> entry in marks)
            {
                currentMarks[entry.Key.subjectName] = entry.Value;
            }

            return true;
        }



        // Check the learner against the selected degree.
        // ALWAYS returns a result: when something is missing the outcome is "More Information Needed",
        // with the reason
        public EligibilityResult checkEligibility()
        {
            refreshMarks();
            EligibilityChecker checker = new EligibilityChecker();

            if (SelectedDegree == null)
            {
                EligibilityResult missing = new EligibilityResult();
                missing.Outcome = AdvisoryOutcome.MoreInformationNeeded;
                missing.Reasons.Add("No qualification has been selected yet.");
                missing.Summary = "More information is needed. Please select a qualification first.";
                lastResult = missing;
                return missing;
            }

            double? interest = getInterestMatchOrNull(SelectedDegree);

            lastResult = checker.Check(SelectedDegree.title,
                                       currentMarks,
                                       SelectedDegree.requirements,
                                       interest);
            return lastResult;
        }

        // Other qualifications the learner could consider, best first.
        // Pass in all the loaded degrees (later: degreeDB.degrees).
        public List<EligibilityResult> getAlternatives()
        {
            try
            {
                loadRequirements();
            }
            catch (Exception)
            {
                return new List<EligibilityResult>();
            }
            refreshMarks();

            Collection<Degree> alldegrees = degreeDB.degrees;
            Dictionary<string, List<ProgrammeRequirement>> byDegree = new Dictionary<string, List<ProgrammeRequirement>>();

            foreach (Degree d in alldegrees)
            {
                if (d == null)
                {
                    continue;
                }

                string title = d.title;
                if (!byDegree.ContainsKey(title))          // a degree may appear under more than one faculty
                {
                    byDegree[title] = new List<ProgrammeRequirement>(d.requirements);
                }
            }

            string selected = "";
            if (SelectedDegree != null)
            {
                selected = SelectedDegree.title;
            }

            return new EligibilityChecker().FindAlternatives(currentMarks, byDegree, selected);
        }

        private void loadRequirements()
        {
            if (requirementDB != null) return;
            requirementDB = new ProgrammeRequirementDB(subjectDB, degreeDB);
        }

        private void refreshMarks()
        {
            if (activeLearner == null) return;

            currentMarks.Clear();
            foreach (LearnerSubject learnerSubject in activeLearner.learnerSubjects)
            {
                if (learnerSubject.subject != null)
                {
                    currentMarks[learnerSubject.subject.subjectName] = learnerSubject.result;
                }
            }
        }

        public List<EligibilityResult> checkSelectionEligibility(out string error)
        {
            error = "";
            List<EligibilityResult> results = new List<EligibilityResult>();

            try
            {
                loadRequirements();
            }
            catch (Exception ex)
            {
                error = "Could not load the qualification requirements: " + ex.Message;
                return results;
            }

            if (SelectedDegree != null)
            {
                results.Add(checkEligibility());
                return results;
            }

            if (SelectedFaculty != null)
            {
                refreshMarks();
                EligibilityChecker checker = new EligibilityChecker();

                foreach (Degree degree in SelectedFaculty.degreesOffered)
                {
                    results.Add(checker.Check(degree.title, currentMarks, degree.requirements, getInterestMatchOrNull(degree)));
                }

                results.Sort((a, b) => a.Outcome != b.Outcome ? a.Outcome.CompareTo(b.Outcome) : a.Gaps.Count.CompareTo(b.Gaps.Count));
            }

            return results;
        }

        // Interest match for the selected degree, or null if it can't be worked out yet
        // (no questionnaire answers, or the degree has no interest categories).
        // Null just means "not assessed": the checker then decides on subjects and marks alone.
        private double? getInterestMatchOrNull(Degree? degree)
        {
            if (degree == null || degree.categories == null)
            {
                return null;
            }

            if (degree.categories.Count == 0)
            {
                return null;
            }

            Dictionary<int, double> scores = calculateInterestScores();      // empty if nothing answered
            if (scores.Count == 0)
            {
                return null;
            }

            List<int> categoryIds = new List<int>();
            foreach (FuturePathGroup10.Interest.InterestCategory c in degree.categories)
            {
                categoryIds.Add(c.interestCategoryID);
            }

            return new FuturePathGroup10.Interest.InterestScorer().CalculateDegreeMatch(scores, categoryIds);
        }

        #endregion
    }

}
