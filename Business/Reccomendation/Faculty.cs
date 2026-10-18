using FuturePathGroup10.Funding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class Faculty : ReferenceData
    {
        public int facultyID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<FundingOption> options { get; private set; }

        // constructor
        public Faculty(int facultyID, string title, string description)
        {
            this.facultyID = facultyID;
            this.description = description;
            this.title = title;
            options = new Collection<FundingOption>();
        }

        public void addFundingOption(FundingOption option)
        {
            if (option != null)
            {
                options.Add(option);
            }
        }

        public Faculty getFaculty()
        {
            return (Faculty)this;
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
