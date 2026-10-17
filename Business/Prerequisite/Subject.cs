using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Prerequisites
{
    public record Subject(string subjectName, string subjectDescription)
    {
        public override string ToString()
        {
            return subjectName;
        }
    }
}
