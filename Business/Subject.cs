using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    public class Subject
    {
        public string subjectName { get; private set; }
        public string subjectDescription { get; private set; }

        public Subject(string subjectName, string subjectDescription)
        {
            this.subjectName = subjectName;
            this.subjectDescription = subjectDescription;
        }

        public string getSubject()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Object obj)
        {
            Subject other = (Subject)obj;
            return subjectName.Equals(other.subjectName);
        }
    }
}
