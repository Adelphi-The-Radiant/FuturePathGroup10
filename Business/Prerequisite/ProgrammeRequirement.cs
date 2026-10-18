using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Reccomendation;

namespace FuturePathGroup10.Prerequisites
{
    public class ProgrammeRequirement : ReferenceData
    {
        public int programmeRequirementID { get; private set;}
        public Subject subject { get; private set; }
        public decimal minRequiredGrade { get; private set; }

        public ProgrammeRequirement(int programmeRequirementID, Subject subject, decimal minRequiredGrade) 
        {
            this.programmeRequirementID = programmeRequirementID;
            this.subject = subject;
            this.minRequiredGrade = minRequiredGrade;
        }

        public bool compareMarks()
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

        public override void add()
        {
            throw new NotImplementedException();
        }

        public int getMinRequiredGrade()
        {
            throw new NotImplementedException();
        }
    }
}
