using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class Degree : ReferenceData
    {
        public int degreeID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }

        // constructor
        public Degree(int degreeID, string title, string description)
        { 
            this.degreeID = degreeID;
            this.title = title;
            this.description = description;
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
