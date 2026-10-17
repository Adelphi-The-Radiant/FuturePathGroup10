using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Interest
{
    public class LearnerInterestResponse
    {
        public int responseID { get; private set; }
        public Learner.Learner learner { get; private set; }
        public string result { get; private set; }
        public InterestQuestionnaire? questionnaire { get; private set; }

        public LearnerInterestResponse(int responseID,Learner.Learner learner, string result)
        {
            this.responseID = responseID;
            this.learner = learner;
            this.result = result;
        }

        public void setQuestionnaire(InterestQuestionnaire questionnaire)
        {
            this.questionnaire = questionnaire;
        }

        public void addResponse(string response)
        {
            throw new NotImplementedException();
        }

        public string getResponse()
        {
            throw new NotImplementedException();
        }
    }
}
