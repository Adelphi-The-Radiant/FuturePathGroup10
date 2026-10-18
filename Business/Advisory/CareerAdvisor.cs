using FuturePathGroup10.Data;
using FuturePathGroup10.Learner;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Advisory
{
    public class CareerAdvisor : AccountHolder
    {
        public int advisorID { get;  set; }
        public string fullName { get;  set; }

        // references to the databases needed to generate the reports
        private LearnerDB learnerDB;
        private AdvisorySessionDB advisorySessionDB;
        private ReccommendationResultDB reccommendationResultDB;

        public CareerAdvisor(int advisorID, string fullName, string username, LearnerDB learnerDB, AdvisorySessionDB advisorySessionDB, ReccommendationResultDB reccommendationResultDB) : base(username)
        {
            this.advisorID = advisorID;
            this.fullName = fullName;

            this.learnerDB = learnerDB;
            this.advisorySessionDB = advisorySessionDB;
            this.reccommendationResultDB = reccommendationResultDB;
        }

        public string generateAdvisingReport(int learnerID)
        {
            Learner.Learner learner = learnerDB.getLearner(learnerID);
            if (learner == null)
            {
                return "Learner with ID " + learnerID + " not found.";
            }

            string header = "Advisory Report For " + learner.fullname + "\nAdvisor: " + this.fullName + "\nDate: " + DateTime.Now.ToShortDateString() + "\n\n";
            string learnerProfile = learner.getFullProfile() + "\n\n";
            var rec = reccommendationResultDB.getAllReccommendations(learnerID);
            string recc = "";
            if (string.IsNullOrEmpty(rec))
            {
                recc += "No reccomendations generated yet.";
            } else
            {
                recc += rec;
            }
            return header + learnerProfile + recc;
        }

        public string generateExceptionReport(int learnerID)
        {
            Learner.Learner learner = learnerDB.getLearner(learnerID);
            if (learner == null)
            {
                return "Learner with ID " + learnerID + " not found.";
            }

            string header = "Exception Report For " + learner.fullname + "\nAdvisor: " + this.fullName + "\nDate: " + DateTime.Now.ToShortDateString() + "\n\n";
            // finding the exception
            string exc = "";
            if (!learner.isProfileComplete()) // has the learner fully competed their profile
            {
                exc += "Learner profile is incomplete.\nPlease check that all details and marks are correctly entered.\n\n";
            }
            if (learner.overallGrade11<40 || learner.overallGrade12<40) // is the learner failing?
            {
                exc += "Learner has critically low marks.\n\n";
            }
            var rec = reccommendationResultDB.getAllReccommendations(learnerID);
            if (string.IsNullOrEmpty(rec))
            {
                exc += "The system was unable to generate any degree reccomendations for this learner.\n\n";
            }

            // returning exc
            return exc;
        }
    }
}
