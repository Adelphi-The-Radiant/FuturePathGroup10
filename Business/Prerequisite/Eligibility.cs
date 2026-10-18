using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business.Prerequisite
{
    public enum AdvisoryOutcome
    {
        StrongMatch,
        PossibleMatch,
        NotCurrentlyEligible,
        MoreInformationNeeded
    }


    public class EligibilityResult
    {
        public const string Disclaimer =
            "This is preliminary guidance only and not an official admission decision. " +
            "The requirements shown are sample information and must be verified with the institution.";

        public AdvisoryOutcome Outcome { get; set; }
        public string DegreeTitle { get; set; } = "";
        public double? InterestMatch { get; set; }
        public List<string> Reasons { get; } = new List<string>();
        public List<string> Gaps { get; } = new List<string>();
        public string Summary { get; set; } = "";
    }


    public class EligibilityChecker
    {
        public const double StrongInterestThreshold = 60.0;

        // learnerMarks:  subject name -> mark (0 to 100)
        // requirements:  the degree's requirement rows (e.g. degree.requirements)
        // interestMatch: 0 to 100, or null when the questionnaire has not been done
        public EligibilityResult Check(string degreeTitle,
                                       IDictionary<string, double> learnerMarks,
                                       IEnumerable<ProgrammeRequirement> requirements,
                                       double? interestMatch)
        {
            EligibilityResult result = new EligibilityResult();
            result.DegreeTitle = degreeTitle;
            result.InterestMatch = interestMatch;

            List<ProgrammeRequirement> reqList = new List<ProgrammeRequirement>();
            if (requirements != null)
            {
                foreach (ProgrammeRequirement r in requirements)
                {
                    reqList.Add(r);
                }
            }

            // Not enough information to check
            if (learnerMarks == null || learnerMarks.Count == 0)
            {
                result.Outcome = AdvisoryOutcome.MoreInformationNeeded;
                result.Reasons.Add("No subjects and marks have been entered yet.");
                Finish(result);
                return result;
            }

            if (reqList.Count == 0)
            {
                result.Outcome = AdvisoryOutcome.MoreInformationNeeded;
                result.Reasons.Add("No requirements are stored for this qualification yet.");
                Finish(result);
                return result;
            }

            // subject names compared without caring about upper or lower case
            Dictionary<string, double> marks = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);
            foreach (KeyValuePair<string, double> m in learnerMarks)
            {
                marks[m.Key] = m.Value;
            }

            //  Group the requirements: rows in the same group are alternatives
            SortedDictionary<int, List<ProgrammeRequirement>> groups = new SortedDictionary<int, List<ProgrammeRequirement>>();
            foreach (ProgrammeRequirement r in reqList)
            {
                if (!groups.ContainsKey(r.requirementGroup))
                {
                    groups[r.requirementGroup] = new List<ProgrammeRequirement>();
                }
                groups[r.requirementGroup].Add(r);
            }

            bool anyMissingSubject = false;
            bool anyMarkGap = false;

            // Check each group
            foreach (KeyValuePair<int, List<ProgrammeRequirement>> entry in groups)
            {
                List<ProgrammeRequirement> alternatives = entry.Value;

                bool mandatory = false;
                foreach (ProgrammeRequirement alt in alternatives)
                {
                    if (alt.isMandatory)
                    {
                        mandatory = true;
                    }
                }

                bool met = false;
                string metText = "";
                ProgrammeRequirement nearest = null;      // the alternative the learner has, but with too low a mark
                double nearestMark = 0;
                double smallestShortfall = double.MaxValue;

                foreach (ProgrammeRequirement alt in alternatives)
                {
                    double mark;
                    if (!marks.TryGetValue(alt.subject.subjectName, out mark))
                    {
                        continue;                         // learner does not have this subject
                    }

                    double minimum = (double)alt.minRequiredGrade;

                    if (mark >= minimum)
                    {
                        met = true;
                        metText = alt.subject.subjectName + ": " + mark + "% meets the minimum of " + minimum + "%.";
                        break;
                    }

                    double shortfall = minimum - mark;
                    if (shortfall < smallestShortfall)
                    {
                        smallestShortfall = shortfall;
                        nearest = alt;
                        nearestMark = mark;
                    }
                }

                if (met)
                {
                    result.Reasons.Add(metText);
                    continue;
                }

                if (nearest != null)
                {
                    // has the subject, mark too low
                    double minimum = (double)nearest.minRequiredGrade;
                    string text = "Your " + nearest.subject.subjectName + " mark (" + nearestMark +
                                  "%) is below the minimum of " + minimum + "%.";

                    if (mandatory)
                    {
                        result.Reasons.Add(text);
                        result.Gaps.Add("Raise " + nearest.subject.subjectName + " from " + nearestMark +
                                        "% to at least " + minimum + "%.");
                        anyMarkGap = true;
                    }
                    else
                    {
                        result.Reasons.Add("Recommended: " + text);
                    }
                }
                else
                {
                    // does not have any of the subjects
                    string needed = DescribeAlternatives(alternatives);

                    if (mandatory)
                    {
                        result.Reasons.Add("You do not have a required subject: " + needed + ".");
                        result.Gaps.Add("Missing required subject: " + needed + ".");
                        anyMissingSubject = true;
                    }
                    else
                    {
                        result.Reasons.Add("Recommended subject not entered: " + needed + ".");
                    }
                }
            }

            // 4. Decide the outcome
            if (anyMissingSubject)
            {
                result.Outcome = AdvisoryOutcome.NotCurrentlyEligible;
            }
            else if (anyMarkGap)
            {
                result.Outcome = AdvisoryOutcome.PossibleMatch;
            }
            else if (interestMatch.HasValue && interestMatch.Value < StrongInterestThreshold)
            {
                result.Outcome = AdvisoryOutcome.PossibleMatch;
                result.Reasons.Add("You meet the requirements, but your interests line up less strongly with this qualification (" +
                                   Math.Round(interestMatch.Value) + "%).");
            }
            else
            {
                result.Outcome = AdvisoryOutcome.StrongMatch;
                if (!interestMatch.HasValue)
                {
                    result.Reasons.Add("Interest alignment was not assessed.");
                }
            }

            Finish(result);
            return result;
        }

        // Other qualifications where the learner is a Strong or Possible Match, best first.
        // requirementsByDegree: degree title -> that degree's requirements (e.g. degree.title -> degree.requirements)
        // Interest is not used here (null), so ranking is by outcome and then by fewest gaps.
        public List<EligibilityResult> FindAlternatives(IDictionary<string, double> learnerMarks,
                                                        IDictionary<string, List<ProgrammeRequirement>> requirementsByDegree,
                                                        string selectedDegreeTitle)
        {
            List<EligibilityResult> alternatives = new List<EligibilityResult>();

            foreach (KeyValuePair<string, List<ProgrammeRequirement>> entry in requirementsByDegree)
            {
                if (entry.Key == selectedDegreeTitle)
                {
                    continue;
                }

                EligibilityResult r = Check(entry.Key, learnerMarks, entry.Value, null);

                if (r.Outcome == AdvisoryOutcome.StrongMatch || r.Outcome == AdvisoryOutcome.PossibleMatch)
                {
                    alternatives.Add(r);
                }
            }

            alternatives.Sort(CompareAlternatives);
            return alternatives;
        }

        private static int CompareAlternatives(EligibilityResult a, EligibilityResult b)
        {
            if (a.Outcome != b.Outcome)
            {
                return a.Outcome.CompareTo(b.Outcome);
            }
            return a.Gaps.Count.CompareTo(b.Gaps.Count);
        }

        // "Mathematics (at least 60%) or Mathematical Literacy (at least 70%)"
        private string DescribeAlternatives(List<ProgrammeRequirement> alternatives)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < alternatives.Count; i++)
            {
                if (i > 0)
                {
                    sb.Append(" or ");
                }
                sb.Append(alternatives[i].subject.subjectName);
                sb.Append(" (at least ");
                sb.Append((double)alternatives[i].minRequiredGrade);
                sb.Append("%)");
            }
            return sb.ToString();
        }

        // Builds the learner-friendly sentence that goes with the outcome
        private void Finish(EligibilityResult result)
        {
            string title = result.DegreeTitle;

            switch (result.Outcome)
            {
                case AdvisoryOutcome.StrongMatch:
                    result.Summary = "You are a strong match for " + title + ". You meet the subject and mark requirements.";
                    break;

                case AdvisoryOutcome.PossibleMatch:
                    if (result.Gaps.Count > 0)
                    {
                        result.Summary = "You are a possible match for " + title +
                                         ". To strengthen your application: " + string.Join(" ", result.Gaps);
                    }
                    else
                    {
                        result.Summary = "You are a possible match for " + title +
                                         ". You meet the requirements, but your interests line up less strongly with it.";
                    }
                    break;

                case AdvisoryOutcome.NotCurrentlyEligible:
                    result.Summary = "You are not currently eligible for " + title + ". " + string.Join(" ", result.Gaps);
                    break;

                default:
                    result.Summary = "More information is needed before " + title + " can be checked. " +
                                     string.Join(" ", result.Reasons);
                    break;
            }
        }
    }
}

