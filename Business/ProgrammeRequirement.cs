using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Business
{
    internal class ProgrammeRequirement : ReferenceData
    {
        private DegreeProgramme programme; 
        private int programmeRequirementID;
        private string subjectName;
        private int minRequiredGrade;

        public ProgrammeRequirement(DegreeProgramme programme) 
        { 
            this.programme = programme;
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
