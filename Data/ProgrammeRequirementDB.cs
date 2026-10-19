using FuturePathGroup10.Funding;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.Marshalling.IIUnknownCacheStrategy;
using static System.Windows.Forms.Design.AxImporter;

namespace FuturePathGroup10.Data
{

    public class ProgrammeRequirementDB : DB
    {
        #region Attributes
        private const string TABLE_PR = "ProgrammeRequirement";
        private const string TABLE_REQUIREMENT = "Requirements";
        private const string TABLE_JOINED = "RequirementsJoined";

        private const string SQL_SELECT_JOINED = "SELECT ProgrammeRequirement.ProgrammeRequirementID AS ProgrammeRequirementID, ProgrammeRequirement.SubjectName AS SubjectName, ProgrammeRequirement.MinRequiredGrade AS MinRequiredGrade, Requirements.DegreeID AS DegreeID " +
            "FROM ProgrammeRequirement LEFT JOIN Requirements ON ProgrammeRequirement.ProgrammeRequirementID = Requirements.ProgrammeRequirementID " +
            "ORDER BY ProgrammeRequirement.ProgrammeRequirementID";
        private const string SQL_SELECT_PR = "SELECT * FROM ProgrammeRequirement";
        private const string SQL_SELECT_REQUIREMENT = "SELECT * FROM Requirements";

        private DataSet dsPR;
        private DataSet dsRequirement;

        private OleDbDataAdapter daPR;
        private OleDbDataAdapter daRequirement;

        private SubjectDB subjectDB;
        private DegreeDB degreeDB;

        public Collection<ProgrammeRequirement> requirements;
        #endregion
        #region Constructor
        public ProgrammeRequirementDB(SubjectDB subjectDB, DegreeDB degreeDB)
        {
            this.subjectDB = subjectDB;
            this.degreeDB = degreeDB;
            requirements = new Collection<ProgrammeRequirement>();

            FillDataSet(SQL_SELECT_PR, TABLE_PR);
            dsPR = dsMain;
            daPR = daMain;
            FillDataSet(SQL_SELECT_REQUIREMENT, TABLE_REQUIREMENT);
            dsRequirement = dsMain;
            daRequirement = daMain;

            FillDataSet(SQL_SELECT_JOINED, TABLE_JOINED);
            LoadRequirements();
        }
        #endregion
        #region Load DataSet

        private void LoadRequirements()
        {
            int requirementID = -1;
            ProgrammeRequirement requirement = null;

            foreach (DataRow row in dsMain.Tables[TABLE_JOINED].Rows)
            {
                int candidateID = Convert.ToInt32(row["ProgrammeRequirementID"]);

                if (requirementID != candidateID)
                {
                    requirementID = candidateID;
                    requirement = null;

                    Subject subject = row["SubjectName"] is DBNull ? null : subjectDB.getSubject(row["SubjectName"].ToString());
                    if (subject != null && row["MinRequiredGrade"] is not DBNull)
                    {
                        requirement = new ProgrammeRequirement(requirementID, subject, Convert.ToDouble(row["MinRequiredGrade"]));
                        requirements.Add(requirement);
                    }
                }

                if (requirement == null || row["DegreeID"] is DBNull) continue;

                Degree degree = degreeDB.getDegree(Convert.ToInt32(row["DegreeID"]));
                degree?.addRequirement(requirement);
            }

        }
        #endregion

        #region Update DataSet
        private void FillDataRow(DataRow row, ProgrammeRequirement requirement)
        {
            row["ProgrammeRequirementID"] = requirement.programmeRequirementID;
            row["SubjectName"] = requirement.subject.subjectName;
            row["MinRequiredGrade"] = requirement.minRequiredGrade;
        }

        private void FillDataRow(DataRow row, ProgrammeRequirement requirement, Degree degree)
        {
            row["ProgrammeRequirementID"] = requirement.programmeRequirementID;
            row["DegreeID"] = degree.degreeID;
        }

        private DataRow FindDataRow(ProgrammeRequirement requirement)
        {
            foreach (DataRow row in dsPR.Tables[TABLE_PR].Rows)
            {
                if (Convert.ToInt32(row["ProgrammeRequirementID"]) == requirement.programmeRequirementID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDataRow(ProgrammeRequirement requirement, Degree degree)
        {
            foreach (DataRow row in dsRequirement.Tables[TABLE_REQUIREMENT].Rows)
            {
                if (Convert.ToInt32(row["ProgrammeRequirementID"]) == requirement.programmeRequirementID && Convert.ToInt32(row["DegreeID"]) == degree.degreeID)
                {
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(ProgrammeRequirement requirement, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsPR.Tables[TABLE_PR].NewRow();
                    FillDataRow(row, requirement);
                    dsPR.Tables[TABLE_PR].Rows.Add(row);

                    requirements.Add(requirement);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(requirement);
                    if (row != null)
                    {
                        FillDataRow(row, requirement);
                    }
                    break;
                case DBOperation.Delete:
                    row = FindDataRow(requirement);
                    if (row != null)
                    {
                        row.Delete();
                        requirements.Remove(requirement);
                    }
                    break;

            }
        }

        public void DataSetChange(ProgrammeRequirement requirement, Degree degree, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsRequirement.Tables[TABLE_REQUIREMENT].NewRow();
                    FillDataRow(row, requirement, degree);
                    dsRequirement.Tables[TABLE_REQUIREMENT].Rows.Add(row);

                    break;

                case DBOperation.Edit:
                    row = FindDataRow(requirement, degree);
                    if (row != null)
                    {
                        FillDataRow(row, requirement, degree);
                    }
                    break;
                case DBOperation.Delete:
                    row = FindDataRow(requirement, degree);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    break;
            }
        }
        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            dsMain = dsPR;
            bool check1 = UpdateDataSource(SQL_SELECT_PR, TABLE_PR, daPR);
            dsPR = dsMain;
            dsMain = dsRequirement;
            bool check2 = UpdateDataSource(SQL_SELECT_REQUIREMENT, TABLE_REQUIREMENT, daRequirement);
            dsRequirement = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            daPR.InsertCommand = new OleDbCommand("INSERT INTO ProgrammeRequirement (ProgrammeRequirementID, SubjectName, MinRequiredGrade) VALUES (?, ?, ?)", cnMain);
            daRequirement.InsertCommand = new OleDbCommand("INSERT INTO Requirements (DegreeID, ProgrammeRequirementID) VALUES (?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daPR.UpdateCommand = new OleDbCommand("UPDATE ProgrammeRequirement SET SubjectName = ?, MinRequiredGrade = ? WHERE ProgrammeRequirementID = ?", cnMain);
            //Not possible anyway
            daRequirement.UpdateCommand = new OleDbCommand("UPDATE Requirements SET ProgrammeRequirementID = ? WHERE ProgrammeRequirementID = ? AND DegreeID = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daPR.DeleteCommand = new OleDbCommand("DELETE FROM ProgrammeRequirement WHERE ProgrammeRequirementID = ?", cnMain);
            daRequirement.DeleteCommand = new OleDbCommand("DELETE FROM Requirements WHERE ProgrammeRequirementID = ? AND DegreeID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daPR.InsertCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));
            daPR.InsertCommand.Parameters.Add(CreateParam("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));
            daPR.InsertCommand.Parameters.Add(CreateParam("@MinRequiredGrade", OleDbType.Double, 8, "MinRequiredGrade"));

            daRequirement.InsertCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
            daRequirement.InsertCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daPR.UpdateCommand.Parameters.Add(CreateParam("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));
            daPR.UpdateCommand.Parameters.Add(CreateParam("@MinRequiredGrade", OleDbType.Double, 8, "MinRequiredGrade"));
            daPR.UpdateCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));

            daRequirement.UpdateCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));
            daRequirement.UpdateCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));
            daRequirement.UpdateCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daPR.DeleteCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));

            daRequirement.DeleteCommand.Parameters.Add(CreateParam("@ProgrammeRequirementID", OleDbType.Integer, 4, "ProgrammeRequirementID"));
            daRequirement.DeleteCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
        }

        #endregion
    }
}
