using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    internal class FundingOption : ReferenceData
    {
        private int optionID;
        private string description;
        /// This should be decimal
        private float amount;

        public List<string> getFundingOptions()
        {
            throw new NotImplementedException();
        }

        // This takes no parameters and returns a string? Weird for a setter
        public string setFundingOptions()
        {
            throw new NotImplementedException();
        }

        public override void add()
        {
            throw new NotImplementedException();
        }

        public override void deactivate()
        {
            throw new NotImplementedException();
        }

        public override void update()
        {
            throw new NotImplementedException();
        }
    }
}
