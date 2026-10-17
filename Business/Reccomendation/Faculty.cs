using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    internal class Faculty : ReferenceData
    {
        private int facultyID { get; private set };
        private string title { get; private set };
        private string description { get; private set };

        // constructor
        public Faculty(int facultyID, string title, string description)
        {
            this.facultyID = facultyID;
            this.description = description;
            this.title = title;
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
