using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;

namespace FuturePathGroup10.Advisory
{
    public class AdvisorySession
    {
        public int sessionID;
        public int learnerID;
        public int advisorID;
        // Should be a more specialized type for dates
        public DateTime date;
        public string notes;

        public AdvisorySession(int sessionID, int learnerID, int advisorID, DateTime date, string notes)
        {
            this.sessionID = sessionID;
            this.learnerID = learnerID;
            this.advisorID = advisorID;
            this.date = date;
            this.notes = notes;
        }

        public void addSubject(Subject subjectName)
        {
            throw new NotImplementedException();
        }

        public void removeSubject(Subject subjectName)
        {
            throw new NotImplementedException();
        }

        public void updateMark()
        {
            throw new NotImplementedException();
        }

        public void validateDuplicateSubjects(Subject subjectName)
        {
            throw new NotImplementedException();
        }

        public string recommendAlternatives()
        {
            throw new NotImplementedException();
        }

        public string getAdvisoryOutcome()
        {
            throw new NotImplementedException();
        }

        public string advisoryOutcomeReason()
        {
            throw new NotImplementedException();
        }

        public string checkEligibility(DegreeProgramme programme)
        {
            throw new NotImplementedException();
        }
    }
}
