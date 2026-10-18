using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class DegreeProgramme
    {
        // updated: DegreeProgramme no longer takes the ID's of the classes but rather an instance of the class itself
        private Faculty faculty;
        private Degree degree;

        public DegreeProgramme getDegreeProgramme(Faculty faculty, DegreeProgramme degree)
        {
            this.degree = degree;
            this.faculty = faculty;
        }

        // Equals method
        public bool Equals(DegreeProgramme other)
        {
            if (other  == null) return false;

            return (this.faculty.facultyID == other.faculty.facultyID && this.degree.degreeID == other.degree.degreeID);
        }

    }
}
