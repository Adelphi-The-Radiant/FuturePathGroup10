using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Data;

namespace FuturePathGroup10.Reccomendation
{
    public class EnrollmentList
    {
        // instance variables
        public Learner.Learner learner { get; private set; }
        public Faculty faculty { get; private set; }
        public DateTime date { get; private set; }
        public List<Learner.Learner> enrollmentList { get; private set; }

        // constructor
        public EnrollmentList(Learner.Learner learner, Faculty faculty, DateTime date)
        {
            this.learner = learner;
            this.faculty = faculty;
            this.date = date;
            // initializing the list
            this.enrollmentList = new List<Learner.Learner>();
        }

        // adds a Learner to the list
        public void addLearnerToList(Learner.Learner l)
        {
            // checking if the Learner entered isn't null
            if (l != null)
            {
                enrollmentList.Add(l); // add to list
            }
        }
    }
}
