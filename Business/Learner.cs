using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    public class Learner
    {
        public int learnerID { get; set; }
        public string username { get; set; }
        public string phoneNumber { get;  set; }
        public string email { get;  set; }
        // This appears as fullName elsewhere (CareerAdvisor), check out the capitalization
        public string fullname { get;  set; }
        public string school { get;  set; }
        public string grade11 { get;  set; }
        public string grade12 { get;  set; }
        public string province { get;  set; }

        public Learner(int learnerID,string username, string phoneNumber, string fullname, string school, string grade11,string grade12, string email, string province)
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
