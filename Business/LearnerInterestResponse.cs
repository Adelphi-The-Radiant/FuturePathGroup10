using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    public class LearnerInterestResponse
    {
        public int responseID { get; private set; }
        public Learner learner { get; private set; }
        public string response { get; private set; }

        public LearnerInterestResponse(int responseID,Learner learner, string response)
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
