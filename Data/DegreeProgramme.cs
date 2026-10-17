using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    internal class DegreeProgramme
    {
        #region Attributes
        public Faculty faculty { get; private set}; 
        public Degree degree { get; private set};
        #endregion

        // constructor
        public DegreeProgramme(Faculty faculty, Degree degree)
        {
            this.faculty = faculty;
            this.degree = degree;
        }

        //equals method
        public bool Equals(DegreeProgramme other)
        {
            return (this.faculty==other.faculty && this.degree==other.degree);
        }
    }
}
