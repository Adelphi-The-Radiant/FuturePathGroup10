using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Funding;
using System.Collections.ObjectModel;

namespace FuturePathGroup10.Reccomendation
{
    public class Degree : ReferenceData
    {
        public int degreeID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<FundingOption> options { get; private set; }

        // constructor
        public Degree(int degreeID, string title, string description)
        { 
            this.degreeID = degreeID;
            this.title = title;
            this.description = description;
            options = new Collection<FundingOption>();
        }

        public void addFundingOption(FundingOption option)
        {
            if (option != null)
            {
                options.Add(option);
            }
        }

        public Degree GetDegree()
        {
            return (Degree)this;
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
