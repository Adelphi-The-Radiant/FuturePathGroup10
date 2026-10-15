using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Prerequisites
{
    internal class GradeLevel
    {
        public float grade11Result { get; private set; }
        public float grade12Result { get; private set; }

        public GradeLevel(float grade11Result, float grade12Result)
        {
            this.grade11Result = grade11Result;
            this.grade12Result = grade12Result;
        }

        public int getResults()
        {
            throw new NotImplementedException();
        }
    }
}
