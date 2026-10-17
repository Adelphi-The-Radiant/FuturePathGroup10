using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Prerequisites
{
    public class LearnerSubject
    {
        public Learner.Learner learner { get; private set; }
        public Subject subject { get; private set; }
        public double result { get; private set; }

        public LearnerSubject(Learner.Learner learner, Subject subject, double result)
        {
            this.learner = learner;
            this.subject = subject;
            this.result = result;
        }


        public bool equals(object obj)
        {
            LearnerSubject other = (LearnerSubject)obj;

            return learner.Equals(other.learner) && subject.Equals(other.subject);

        }
        
    }
}
