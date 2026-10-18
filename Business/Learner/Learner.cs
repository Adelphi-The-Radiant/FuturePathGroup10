using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Interest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
namespace FuturePathGroup10.Learner
{
    public class Learner : AccountHolder
    {
        public int learnerID { get;  set; }
        public string phoneNumber { get; private set; }
        public string email { get; private set; }
        // This appears as fullName elsewhere (CareerAdvisor), check out the capitalization
        public string fullname { get; private set; }
        public string school { get; private set; }
        public float overallGrade11 { get; private set; }
        public float overallGrade12 { get; private set; }
        public string province { get; private set; }

        private Collection<LearnerSubject> learnerSubjects;
        private Collection<LearnerInterestResponse> LIRs;
        private Collection<InterestQuestionnaire>? questionnaires;

        public Learner(int learnerID,string username, string phoneNumber, string fullname, string school, string grade11, string grade12, string email, string province) : base(username)
        {
            this.learnerID = learnerID;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.fullname = fullname;
            this.school = school;
            this.overallGrade11 = Convert.ToInt32(grade11);
            this.overallGrade12 = Convert.ToInt32(grade12);
            this.province = province;
            this.learnerSubjects = new Collection<LearnerSubject>();
            this.LIRs = new Collection<LearnerInterestResponse>();
        }



        public void addLearnerSubject(LearnerSubject learnerSubject)
        {
            learnerSubjects.Add(learnerSubject);
        }

        public void addLIR(LearnerInterestResponse LIR)
        {
            LIRs.Add(LIR);
        }

        //in the case of a grade11 learner they won't have grade12 overall
        public void setCurrrentGrade(string currentGrade)
        {   //assuming null will be turned to zero
            if (overallGrade11 == 0)
                overallGrade12 = Convert.ToInt32(currentGrade);
            else if (overallGrade12 == 0)
                overallGrade11 = Convert.ToInt32(currentGrade);
        }

        public string getFullProfile()
        {
            StringBuilder profile = new StringBuilder();
            profile.AppendLine("Name: " + (string.IsNullOrWhiteSpace(fullname) ? "Not provided" : fullname));
            profile.AppendLine("School: " + school);
            profile.AppendLine("Province: " + province);
            if (!string.IsNullOrWhiteSpace(phoneNumber)) profile.AppendLine("Phone: " + phoneNumber);
            if (!string.IsNullOrWhiteSpace(email)) profile.AppendLine("Email: " + email);
            return profile.ToString();
        }

        public void addSubjectMark(Subject subject, int mark)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(Object obj)
        {
            Learner other = (Learner)obj;
            return learnerID.Equals(other.learnerID);
        }

        public override int GetHashCode()
        {
            return learnerID.GetHashCode();

        }

        public bool isProfileComplete()
        {
            return !string.IsNullOrWhiteSpace(fullname) &&
                 !string.IsNullOrWhiteSpace(phoneNumber) &&
                 !string.IsNullOrWhiteSpace(email) &&
                 !string.IsNullOrWhiteSpace(school) &&
                 !string.IsNullOrWhiteSpace(province) &&
                 overallGrade11 > 0;
        }

    }
}
