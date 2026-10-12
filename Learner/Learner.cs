using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Learner
{
    internal class Learner
    {
        private int learnerID;
        private string username;
        private string phoneNumber;
        // This appears as fullName elsewhere (CareerAdvisor), check out the capitalization
        private string fullname;
        private string school;
        private string currentGrade;
        private string province;

        public void setCurrentGrade(string currentGrade)
        {
            throw new NotImplementedException();
        }

        public bool isProfileComplete()
        {
            throw new NotImplementedException();
        }

        public string getFullProfile()
        {
            throw new NotImplementedException();
        }

        public void addSubjectMark(Subject subject, int mark)
        {
            throw new NotImplementedException();
        }
    }
}
