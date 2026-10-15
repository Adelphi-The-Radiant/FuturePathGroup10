using FuturePathGroup10.Business;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    internal class AdvisorySession
    {
        private int sessionID;
        private int learnerID;
        private int advisorID;
        // Should be a more specialized type for dates
        private string date;
        private string notes;

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
