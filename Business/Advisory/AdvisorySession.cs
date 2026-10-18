using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;
using System.Collections.ObjectModel;
using FuturePathGroup10.Data;

namespace FuturePathGroup10.Advisory
{
    public class AdvisorySession
    {
        public int sessionID;
        public Learner.Learner learner;
        public CareerAdvisor advisor;
        public DateTime date;
        public string notes;

        private Collection<LearnerSubject> subjects;
        private Collection<DegreeProgramme> programmes;

        public AdvisorySession(int sessionID, Learner.Learner learner, CareerAdvisor advisor, DateTime date, string notes, SubjectDB subjectDB)
        {
            this.sessionID = sessionID;
            this.learner = learner;
            this.advisor = advisor;
            this.date = date;
            this.notes = notes;

            this.subjects = subjectDB.learnerSubjects;
            this.programmes = new Collection<DegreeProgramme>();
        }

        public void addSubject(Subject subjectName)
        {
            // check if it's a duplicate
            validateDuplicateSubjects(subjectName);

            // creates a Learner
            LearnerSubject subject = new LearnerSubject(learner, subjectName, 0);
            subjects.Add(subject);
        }

        public void removeSubject(Subject subjectName)
        {
            LearnerSubject remove = null;
            foreach (LearnerSubject subject in subjects)
            {
                if (subject.subject == subjectName)
                {
                    remove = subject;
                    break;
                }
            }

            if (remove ==  null)
            {
                MessageBox.Show(subjectName.subjectName + " was not found.");
            } else
            {
                subjects.Remove(remove);
                MessageBox.Show(subjectName.subjectName + " successfully removed.");
            }
        }

        public void updateMark(Subject subjectName, double mark)
        {
            LearnerSubject update = null;
            foreach (LearnerSubject subject in subjects)
            {
                if (subject.subject == subjectName)
                {
                    update = subject;
                    break;
                }
            }
            if (update == null)
            {
                MessageBox.Show(subjectName.subjectName + " was not found.");
            }
            else
            {
                update.result = mark;
                MessageBox.Show(subjectName.subjectName + " mark successfully updated.");
            }
        }

        public void validateDuplicateSubjects(Subject subjectName)
        {
            foreach (LearnerSubject subject in subjects)
            {
                if (subject.subject.Equals(subjectName))
                {
                    MessageBox.Show(subjectName.subjectName + " has already been added.");
                    return;
                }
            }
        }

        public string recommendAlternatives()
        {
            string output = "";
            
            foreach (var degree in programmes)
            {
                output += degree.ToString();
            }

            if (string.IsNullOrEmpty(output))
            {
                return "Can not reccomend alternatives.";
            }
            return output;
        }

        public string getAdvisoryOutcome()
        {
            
        }

        public string advisoryOutcomeReason()
        {
            throw new NotImplementedException();
        }

        public string checkEligibility(DegreeProgramme programme)
        {
            double total = 0;

            foreach (var sub in subjects)
            {
                total += sub.result;
            }
            total = total / subjects.Count;

            bool elig = false;
            foreach (var prg in programme.degree.requirements)
            {
                if (prg.minRequiredGrade <  total)
                {
                    elig = true;
                } else
                {
                    elig = false;
                }
            }

            if (!elig)
            {
                return "Not Eligible";
            } else
            {
                return "Eligible";
            }
        }
    }
}
