using FuturePathGroup10.Data;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Prerequisites
{
    public class ProgrammeRequirement : ReferenceData
    {
        public int programmeRequirementID { get; private set; }
        public Subject subject { get; private set; }
        public double minRequiredGrade { get; private set; }
        public int requirementGroup { get; private set; }
        public bool isMandatory { get; private set; }

        private ProgrammeRequirementDB programmeRequirementDB;
        private Degree degree;
        private int programmeRequirementID1;
        private int programmeRequirementID2;
        private bool v;

        public ProgrammeRequirement(int programmeRequirementID, Subject subject, double minRequiredGrade,
                                    int requirementGroup, bool isMandatory, ProgrammeRequirementDB programmeRequirementDB, Degree degree)
        {
            this.programmeRequirementID = programmeRequirementID;
            this.subject = subject;
            this.minRequiredGrade = minRequiredGrade;
            this.requirementGroup = requirementGroup;
            this.isMandatory = isMandatory;

            this.programmeRequirementDB = programmeRequirementDB;
            this.degree = degree;
        }

        //  the requirement gets its own group and is mandatory.
        public ProgrammeRequirement(int programmeRequirementID, Subject subject, double minRequiredGrade)
            : this(programmeRequirementID, subject, minRequiredGrade, programmeRequirementID, true)
        {
        }

        public ProgrammeRequirement(int programmeRequirementID1, Subject subject, double minRequiredGrade, int programmeRequirementID2, bool v)
        {
            this.programmeRequirementID1 = programmeRequirementID1;
            this.subject = subject;
            this.minRequiredGrade = minRequiredGrade;
            this.programmeRequirementID2 = programmeRequirementID2;
            this.v = v;
        }

        public bool compareMarks(double mark)
        {
            return mark >= this.minRequiredGrade;
        }

        public override void deactivate()
        {
            try
            {
                programmeRequirementDB.DataSetChange(this, degree, DB.DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Faculty: " + ex.Message);
            }
        }

        public override void update()
        {
            try
            {
                programmeRequirementDB.DataSetChange(this, degree, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Faculty: " + ex.Message);
            }
        }

        public override void add()
        {
            try
            {
                programmeRequirementDB.DataSetChange(this, degree, DB.DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Faculty: " + ex.Message);
            }
        }

        public int getMinRequiredGrade()
        {
            return (int)minRequiredGrade;
        }
    }
}
