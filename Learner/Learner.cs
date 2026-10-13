using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Learner
{
    internal class Learner
    {
        private int learnerID { get;}
        private string username { get;}
        private string phoneNumber { get; }
        private string email { get; }
        // This appears as fullName elsewhere (CareerAdvisor), check out the capitalization
        private string fullname { get; }
        private string school { get; }
        private string grade11 { get; }
        private string grade12 { get; }
        private string province { get; }

        public Learner(int learnerID,string username, string phoneNumber, string fullname, string school, string grade11, string grade12, string email, string province)
        {
            this.learnerID = learnerID;
            this.username = username;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.fullname = fullname;
            this.school = school;
            this.grade11 = grade11;
            this.grade12 = grade12;
            this.province = province;
        }

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
