using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Data;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Reccomendation;

namespace FuturePathGroup10.Advisory
{
    internal class AdvisorController
    {
        private LearnerDB learnerDB;
        private AdvisorySessionDB advisorySessionDB;
        private ReccommendationResultDB reccommendationResultDB;

        // constructor
        public AdvisorController(LearnerDB learnerDB, AdvisorySessionDB advisorySessionDB, ReccommendationResultDB reccommendationResultDB)
        {
            this.learnerDB = learnerDB;
            this.reccommendationResultDB = reccommendationResultDB;
            this.advisorySessionDB = advisorySessionDB;
        }

        public string viewAdvisoryOutcome(int learnerID)
        {
            // gets the learner
            Learner.Learner learner = learnerDB.getLearner(learnerID);  
            // checks if the learner exists
            if (learner == null)
            {
                return "Learner with ID " + learnerID + " does not exist.";
            }
            string temp = ""
            List<ReccommendationResult> reccomend = reccommendationResultDB.GetReccommendationResult(learnerID);
            if (reccomend.Count > 0)
            {
                foreach (var result in reccomend)
                {
                    temp += result.ToString();
                }
                return temp;
            }
            return ("No advisory outcomes generated for " + learner.fullname + " yet.");
        }

        public string viewLearnerInformation(int learnerID)
        {
            Learner.Learner learner = learnerDB.getLearner(learnerID);
            // checks if the learner exists
            if (learner == null)
            {
                MessageBox.Show("Learner with ID " + learnerID + " does not exist.");
                return "";
            }
            return "Learner ID: " + learnerID + "\nName: " + learner.fullname + "\nEmail: " + learner.email + "\nPhone: " + learner.phoneNumber + "\nSchool: " + learner.school;
        }

        public string viewExportSummary(int learnerID)
        {
            Learner.Learner learner = learnerDB.getLearner(learnerID);
            // checks if the learner exists
            if (learner == null)
            {
                MessageBox.Show("Learner with ID " + learnerID + " does not exist.");
                return "";
            }
            string summary = reccommendationResultDB.getAllReccommendations(learnerID);
            if (string.IsNullOrEmpty(summary))
            {
                MessageBox.Show("No summary generated for " + learner.fullname + "yet.");
                return "";
            }
            return summary;
        }

        public void exportSummary(string fullReport)
        {
            try
            {
                string file = "AdvisoryReport_" + DateTime.Now + ".txt";
                System.IO.File.WriteAllText(file, fullReport);
                MessageBox.Show("Report successfully exported: " + file);
            } catch (Exception e)
            {
                MessageBox.Show("Error exporting report");
            }
        }

        public string viewAdvisorySession(int learnerID)
        {
            Learner.Learner learner = learnerDB.getLearner(learnerID);
            // checks if the learner exists
            if (learner == null)
            {
                MessageBox.Show("Learner with ID " + learnerID + " does not exist.");
                return "";
            }
            string temp = "";
            List <AdvisorySession> session = advisorySessionDB.getAllSessions(learnerID);
            if (session.Count > 0)
            {
                foreach (var result in session)
                {
                    temp += result.ToString();
                }
                return temp;
            }
            return ("No advisory outcomes generated for " + learner.fullname + " yet.");
        }
    }
}
