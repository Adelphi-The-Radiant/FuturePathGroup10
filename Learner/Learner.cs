using FuturePathGroup10.Prerequisites;
using FuturePathGroup10;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace FuturePathGroup10.Learner
{
    public class Learner : AccountHolder
    {
        public int learnerID { get; private set; }
        public string phoneNumber { get; private set; }
        public string email { get; private set; }
        // This appears as fullName elsewhere (CareerAdvisor), check out the capitalization
        public string fullname { get; private set; }
        public string school { get; private set; }
        internal GradeLevel overallGrade { get; private set; }
        public string province { get; private set; }

        private Collection<LearnerSubject> learnerSubjects ;

        public Learner(int learnerID,string username, string phoneNumber, string fullname, string school, string grade11, string grade12, string email, string province) : base(username)
        {
            this.learnerID = learnerID;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.fullname = fullname;
            this.school = school;
            this.overallGrade = new GradeLevel(Convert.ToSingle(grade11), Convert.ToSingle(grade12));
            this.province = province;
            this.learnerSubjects = new Collection<LearnerSubject>();
        }



        public void addLearnerSubject(LearnerSubject learnerSubject)
        {
            this.learnerSubjects.Add(learnerSubject);
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

        public bool Equals(Object obj)
        {
            Learner other = (Learner)obj;
            return learnerID.Equals(other.learnerID);
        }
    }
}
