using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    public class CareerAdvisor : AccountHolder
    {
        public int advisorID { get;  set; }
        public string fullName { get;  set; }

        public CareerAdvisor(int advisorID, string fullName, string username) : base(username)
    {
            this.advisorID = advisorID;
            this.fullName = fullName;
        }

        public string generateAdvisingReport(int learnerID)
        {
            throw new NotImplementedException();
        }

        public void generateExceptionReport(int learnerID)
        {
            throw new NotImplementedException();
        }
    }
}
