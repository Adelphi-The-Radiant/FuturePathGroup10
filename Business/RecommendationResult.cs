using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    internal class RecommendationResult
    {
        private int learnerID;
        private int degreeID;
        // Should be a specialized type for dates
        private string date;
        private string results;

        public string getRecommendationResults()
        {
            throw new NotImplementedException();
        }

        public double calculateOverallScore()
        {
            throw new NotImplementedException();
        }

        public string explainOutcome()
        {
            throw new NotImplementedException();
        }
    }
}
