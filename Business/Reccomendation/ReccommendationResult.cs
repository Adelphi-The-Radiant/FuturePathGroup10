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
