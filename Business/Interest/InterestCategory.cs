using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Interest
{
    public class InterestCategory : ReferenceData
    {
        public int interestCategoryID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }

        public InterestCategory(int interestCategoryID,string title,string description)
        {
            this.interestCategoryID = interestCategoryID;
            this.title = title;
            this.description = description;
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
