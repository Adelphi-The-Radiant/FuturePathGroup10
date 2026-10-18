using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Funding
{
    public class FundingOption : ReferenceData
    {
        public int optionID { get; private set; }
        public string description { get; private set; }
        public double amount { get; private set; }

        public FundingOption (int optionID,string description,double amount) : base()
        {
            this.optionID = optionID;
            this.description = description;
            this.amount = amount;
        }

        public override bool Equals(object? obj)
        {
            if (obj is FundingOption)
            {
                FundingOption other = (FundingOption)obj;
                return optionID == other.optionID;
            }
            return false;
        }
        public string  optionToString()
        {
            return description +": R"+ amount;
        }

        public Collection<string> getFundingOptions()
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
