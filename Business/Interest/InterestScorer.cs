
    using System;
    using System.Collections.Generic;
    using System.Linq;

    namespace FuturePathGroup10.Interest
    {
        // Pure calculation: no database, no forms. The controller loads the data and hands it in.
        public class InterestScorer
        {
            public const int MaxAnswer = 5;

            // Returns interestCategoryID -> score as a percentage (0 to 100).
            // questions: each InterestQuestion should already know its categories.
            public Dictionary<int, double> CalculateCategoryScores(
                IEnumerable<InterestQuestion> questions, Dictionary<int, int> answers)
            {
                Dictionary<int, int> totals = new Dictionary<int, int>();   // sum of answers per category
                Dictionary<int, int> counts = new Dictionary<int, int>();   // number of answers per category

                foreach (InterestQuestion q in questions)
                {
                    if (!answers.TryGetValue(q.questionID, out int answer))
                        continue;

                    List<int> categoryIds = new List<int>();

                    if (q.categories.Count == 0)
                    {
                        categoryIds.Add(q.interestCategoryID);
                    }
                    else
                    {
                        foreach (InterestCategory c in q.categories)
                        {
                            categoryIds.Add(c.interestCategoryID);
                        }
                    }

                    foreach (int id in categoryIds)
                    {
                        totals[id] = totals.GetValueOrDefault(id) + answer;
                        counts[id] = counts.GetValueOrDefault(id) + 1;
                    }
                }

                Dictionary<int, double> scores = new Dictionary<int, double>();
                foreach (int id in totals.Keys)
                    scores[id] = totals[id] * 100.0 / (counts[id] * MaxAnswer);

                return scores;
            }

            // Interest match for one degree: the average score over the categories mapped to that degree.
            // Returns 0 if the learner has no score in any of the degree's categories.
            public double CalculateDegreeMatch(Dictionary<int, double> categoryScores, IEnumerable<int> degreeCategoryIds)
            {
                double total = 0;
                int count = 0;

                foreach (int id in degreeCategoryIds)
                {
                    if (categoryScores.ContainsKey(id))
                    {
                        total = total + categoryScores[id];
                        count = count + 1;
                    }
                }

                if (count == 0) return 0;
                return total / count;
            }
        }
    }
