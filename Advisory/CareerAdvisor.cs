using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Advisory
{
    internal class CareerAdvisor : AccountHolder
    {
        private int advisorID;
        private string username;
        private string fullName;

        public CareerAdvisor(string username) : base(username)
        {

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
