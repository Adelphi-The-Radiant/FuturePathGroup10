using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class ReccommendationResult
    {
        public Learner.Learner learner { get; private set; }
        public Degree degree { get; private set; }
        public DateTime date { get; private set; }
        public string results { get; private set; }

        public ReccommendationResult(Learner.Learner learner,Degree degree,DateTime date,string results)
        {
            this.learner = learner;
            this.degree = degree;
            this.date = date;
            this.results = results;
        }

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
