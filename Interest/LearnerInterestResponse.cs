using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Interest
{
    public class LearnerInterestResponse
    {
        public int responseID { get; private set; }
        public Learner.Learner learner { get; private set; }
        public string response { get; private set; }

        public LearnerInterestResponse(int responseID,Learner.Learner learner, string response)
        {
            this.responseID = responseID;
            this.learner = learner;
            this.response = response;
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
