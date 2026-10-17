using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    internal class EnrollmentList
    {
        // instance variables
        private Learner.Learner learner { get; private set };
        private Faculty faculty { get; private set};
        private DateTime date { get; private set };
        private List<Learner> enrollmentList { get; private set };

        // constructor
        public EnrollmentList(Learner.Learner learner, Faculty faculty, DateTime date)
        {
            this.learner = learner;
            this.faculty = faculty;
            this.date = date;
            // initializing the list
            this.enrollmentList = new List<Learner>();
        }

        // adds a Learner to the list
        public void addLearnerToList(Learner l)
        {
            // checking if the Learner entered isn't null
            if (l != null)
            {
                enrollmentList.Add(l); // add to list
            }
        }
    }
}
