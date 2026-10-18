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
            this.degree = degree; // reccomended degree
            this.date = date;
            this.results = results; // explanation of why that degree was recommended
        }

        public Degree getRecommendationResults()
        {
            return degree;
        }

        // how are we supposed to get this????
        public double calculateOverallScore()
        {
            throw new NotImplementedException();
        }

        public string explainOutcome()
        {
            // if no results are given
            if (string.IsNullOrEmpty(results))
            {
                return "No results given.";
            }
            return results;
        }
    }
}
