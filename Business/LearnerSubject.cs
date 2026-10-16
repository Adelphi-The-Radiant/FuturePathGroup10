using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    public class LearnerSubject
    {
        public Learner learner { get; private set; }
        public Subject subject { get; private set; }
        public float result { get; private set; }

        public LearnerSubject(Learner learner, Subject subject, float result)
        {
            this.learner = learner;
            this.subject = subject;
            this.result = result;
        }

        public void setMark(float result)
        {
            throw new NotImplementedException();
        }

        //Either the mark is between 0 and 1 or its between 0 and 100. Pick a standard and keep to it.
        public void validateMark(float result)
        {
            throw new NotImplementedException();
        }

        public bool equals(object obj)
        {
            LearnerSubject other = (LearnerSubject)obj;

            return learner.Equals(other.learner) && subject.Equals(other.subject);

        }
        
    }
}
