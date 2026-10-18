using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Reccomendation;
using System.Collections.ObjectModel;

namespace FuturePathGroup10.Interest
{
    public class InterestCategory : ReferenceData
    {
        public int interestCategoryID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<Degree> degrees { get; private set; }

        public InterestCategory(int interestCategoryID,string title,string description)
        {
            this.interestCategoryID = interestCategoryID;
            this.title = title;
            this.description = description;
            degrees = new Collection<Degree>();
        }

        public void addDegree(Degree degree)
        {
            if (degree != null)
            {
                degrees.Add(degree);
            }
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
