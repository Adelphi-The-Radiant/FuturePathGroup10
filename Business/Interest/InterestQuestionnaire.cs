using FuturePathGroup10.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Interest
{
    public class InterestQuestionnaire
    {
        public LearnerInterestResponse LIR { get; private set; }
        public Collection<InterestQuestion> questions { get; private set; }
        public string response { get; private set; }

        public InterestQuestionnaire(LearnerInterestResponse LIR, string response)
        {
            this.LIR = LIR;
            questions = new Collection<InterestQuestion>();
            this.response = response;
        }

        public void addQuestion(InterestQuestion question)
        {
            questions.Add(question);
        }

        // not sure what to do here...
        public string mapToInterestAlignment()
        {
            if (string.IsNullOrEmpty(response))
            {
                return "Undetermined";
            }
            return "Primary Interest Alignment: " + response;
        }
    }
}
