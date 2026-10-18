using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class RecommendationResult
    {
        public Learner.Learner learner;
        public Degree degree;
        public DateTime date;
        public string results;

        public RecommendationResult(Learner.Learner learner,Degree degree,DateTime date,string results)
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
