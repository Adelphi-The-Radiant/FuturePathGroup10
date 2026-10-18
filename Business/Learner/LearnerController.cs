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
        private LearnerDB learnerDB;

        private SubjectDB subjectDB;

        private FacultyDB facultyDB;

        private DegreeDB degreeDB;

        public Faculty? SelectedFaculty { get; private set; }

        public DegreeProgramme? Programme { get; private set; }

        public Learner? activeLearner { get; private set; }

        private LearnerInterestResponseDB lirDB;
        private InterestQuestionnaireDB questionnaireDB;
        private InterestCategoryDB categoryDB;


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
        private Dictionary<string, double> currentMarks =
            new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
        public EligibilityResult lastResult { get; private set; }
        #endregion

        #region Create and Update learner profile
        //this should be called after register
        public bool createProfile(Learner learner, out string error)
        {
            error = Validate(learner);
            if (error != "") return false;

            try
            {
                // assigning the next free ID
                int nextId = 1;
                foreach (Learner existing in learnerDB.learners)
                {
                    if (existing.learnerID >= nextId)
                    {
                        nextId = existing.learnerID + 1;
                    }
                }

                learner.learnerID = nextId;

                learnerDB.DataSetChange(learner, DB.DBOperation.Add);        
                if (!learnerDB.UpdateDataSource(DB.DBOperation.Add))         
                {
                    error = "Could not save the profile.";
                    return false;
                }

                learnerDB.learners.Add(learner);    
                activeLearner = learner;
                return true;
            }
            catch (Exception ex)
            {
                error = "Could not create profile: " + ex.Message;
                return false;
            }
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

        public bool selectDegreeProgramme(Reccomendation.DegreeProgramme programme, out string error)
        {
            error = "";

            if (programme == null)
            {
                error = "Please choose a valid degree programme.";
                return false;
            }

            Programme = programme;
            return true;
        }
        #endregion

        #region Marks
        public bool captureMarks(Dictionary<Subject, double> marks, out string error)
        {
            error = "";

            if (activeLearner == null)
            {
                error = "Create or load a learner first.";
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
                error = "Create or load a learner first.";
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
            
            return new  Interest.InterestScorer().CalculateCategoryScores(questionnaireDB.questions, answers);
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

            if (Programme == null)
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
            foreach (InterestCategory c in Programme.categories)
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
            FundingOptionDB fundingDB = new FundingOptionDB(facultyDB, degreeDB);

            return fundingDB.options;
        }
        #endregion

        #region Eligibility
        public EligibilityResult checkEligibility(Dictionary<string, double> marks, double? interestMatch)
        {
            if (Programme == null)
            {
                return new EligibilityChecker().Check("", marks, null, interestMatch);   // gives More Information Needed
            }
            return new EligibilityChecker().Check(Programme.degree.title, marks,
                                                  Programme.degree.requirements, interestMatch);
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
            EligibilityChecker checker = new EligibilityChecker();

            if (Programme == null || Programme.degree == null)
            {
                EligibilityResult missing = new EligibilityResult();
                missing.Outcome = AdvisoryOutcome.MoreInformationNeeded;
                missing.Reasons.Add("No qualification has been selected yet.");
                missing.Summary = "More information is needed. Please select a qualification first.";
                lastResult = missing;
                return missing;
            }

            double? interest = getInterestMatchOrNull();

            lastResult = checker.Check(Programme.degree.title,
                                       currentMarks,
                                       Programme.degree.requirements,
                                       interest);
            return lastResult;
        }

        // Other qualifications the learner could consider, best first.
        // Pass in all the loaded programmes (later: degreeDB.programmes).
        public List<EligibilityResult> getAlternatives(IEnumerable<DegreeProgramme> allProgrammes)
        {
            Dictionary<string, List<ProgrammeRequirement>> byDegree = new Dictionary<string, List<ProgrammeRequirement>>();

            foreach (DegreeProgramme p in allProgrammes)
            {
                if (p == null || p.degree == null)
                {
                    continue;
                }

                string title = p.degree.title;
                if (!byDegree.ContainsKey(title))          // a degree may appear under more than one faculty
                {
                    byDegree[title] = new List<ProgrammeRequirement>(p.degree.requirements);
                }
            }

            string selected = "";
            if (Programme != null && Programme.degree != null)
            {
                selected = Programme.degree.title;
            }

            return new EligibilityChecker().FindAlternatives(currentMarks, byDegree, selected);
        }

        // Interest match for the selected degree, or null if it can't be worked out yet
        // (no questionnaire answers, or the degree has no interest categories).
        // Null just means "not assessed": the checker then decides on subjects and marks alone.
        private double? getInterestMatchOrNull()
        {
            if (Programme == null || Programme.degree == null || Programme.degree.categories == null)
            {
                return null;
            }

            if (Programme.degree.categories.Count == 0)
            {
                return null;
            }

            Dictionary<int, double> scores = calculateInterestScores();      // empty if nothing answered
            if (scores.Count == 0)
            {
                return null;
            }

            List<int> categoryIds = new List<int>();
            foreach (FuturePathGroup10.Interest.InterestCategory c in Programme.degree.categories)
            {
                categoryIds.Add(c.interestCategoryID);
            }

            return new FuturePathGroup10.Interest.InterestScorer().CalculateDegreeMatch(scores, categoryIds);
        }

        #endregion
    }

}
