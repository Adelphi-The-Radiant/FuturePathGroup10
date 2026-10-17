using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FuturePathGroup10.Reccomendation;
using FuturePathGroup10.Learner;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{
    public class DegreeDB : DB
    {
        #region Attributes
        // join faculty and degreeprogramme
        private const string SQL_SELECT_DEGREE_JOIN = "SELECT * FROM " +
            "(SELECT * FROM DegreeProgramme LEFT JOIN Faculty ON DegreeProgramme.FacultyID = Faculty.FacultyID ORDER BY DegreeProgramme.FacultyID) " +
            "AS DegreeJoined";
        private const string JOINED_TABLE = "DegreeJoined";

        private const string TABLE_DEGREE = "Degree";
        private const string TABLE_DEGREEPROGRAMME= "DegreeProgramme";

        private const string SQL_SELECT_DEGREE = "SELECT * FROM Degree";
        private const string SQL_SELECT_DEGREEPROGRAMME = "SELECT * FROM DegreeProgramme";

        public Collection<Degree> degrees { get; private set; }
        public Collection<DegreeProgramme> programmes { get; private set; }

        public DataSet dsDegree;
        public DataSet dsDegreeProgramme;

        public OleDbDataAdapter daDegree;
        public OleDbDataAdapter daDegreeProgramme;

        private FacultyDB facultyDB;
        #endregion

        #region Constructor
        public DegreeDB(FacultyDB facultyDB) :base()
        {
            degrees = new Collection<Degrees>();
            programmes = new Collection<DegreeProgramme>();
            this.facultyDB = facultyDB;

            FillDataSet(SQL_SELECT_DEGREEPROGRAMME, DEGREEPROGRAMME_TABLE);
            dsDegreeProgramme = dsMain;
            daDegreeProgramme = daMain;

            FillDataSet(SQL_SELECT_DEGREE, DEGREE_TABLE);
            dsDegree = dsMain;
            dsDegreeProgramme = daMain;

            //dsMain and daMain remain for the joined table
            FillDataSet(SQL_SELECT_EGREE_JOIN, JOINED_TABLE);

            LoadDegreeProgrammes();
        }
        #endregion

        #region Load Degree Programmes
        private void LoadDegreeProgrammes()
        {
            int degreeID = -1;
            string title;
            string description;

            Degree degree = null;
            DegreeProgramme degreeProgramme;
            Faculty faculty;

            foreach (DataRow row in dsMain.Tables[JOINED_TABLE].Rows)
            {
                int candidateID = Convert.ToInt32(row["FacultyID"]);

                //Should ALWAYS activate on the first loop unless the subjects name is Not A String
                if (candidateID != facultyID)
                {
                    facultyID = candidateID;
                    faculty = facultyDB.getFaculty(candidateID);
                    title = (string)row["Title"];
                    int dID = Convert.ToInt32(row["DegreeID"]);
                    degreeID = dID;
                    title = (string)row["Title"];
                    description = (string)row["Description"];

                    degree = new Degree(degreeID, title, description);
                    degrees.Add(degree);
                }

                degreeProgramme = new DegreeProgramme(faculty, degree);
                degreeProgrammes.Add(degreeProgramme);
            }
        }
        #endregion

        #region Dataset Updates
        private void FillDegreeProgrammeRow(DataRow row, DegreeProgramme degreeProgramme)
        {
            row["FacultyID"] = degreeProgramme.faculty.FacultyID;
            row["DegreeID"] = degreeProgramme.degree.DegreeID;
        }

        private void FillDegreeRow(DataRow row, DegreeProgramme degreeProgramme)
        {
            row["DegreeID"] = degreeProgramme.degree.DegreeID;
            row["Title"] = degreeProgramme.degree.Title;
            row["Description"] = degreeProgramme.degree.Description;

        }

        private DataRow FindDegreeProgrammeRow(DegreeProgramme degreeProgramme)
        {
            for (int i = 0; i < dsDegreeProgramme.Tables[DEGREEPROGRAMME_TABLE].Rows.Count; i++)
            {
                DataRow row = dsDegreeProgramme.Tables[DEGREEPROGRAMME_TABLE].Rows[i];
                if (Convert.ToInt32(row["FacultyID"]) == degreeProgramme.faculty.FacultyID && Convert.ToInt32(row["DegreeID"]) == degreeProgramme.degree.DegreeID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDegreeRow(Degree degree)
        {
            for (int i = 0; i < dsDegree.Tables[DEGREE_TABLE].Rows.Count; i++)
            {
                DataRow row = dsDegree.Tables[DEGREE_TABLE].Rows[i];
                if (Convert.ToInt32(row["DegreeID"]) == degree.DegreeID)
                {
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(DegreeProgramme degree, DBOperation operation)
        {
            DataRow rowDegreeProgramme;
            DataRow rowDegree;
            switch (operation)
            {
                case DBOperation.Add:
                    rowDegreeProgramme = dsDegreeProgramme.Tables[DEGREEPROGRAMME_TABLE].NewRow();
                    rowDegree = dsDegree.Tables[DEGREE_TABLE].NewRow();
                    FillDegreeProgrammeRow(rowDegreeProgramme, degreeProgramme);
                    FillDegreeRow(rowDegree, degreeProgramme);

                    dsDegreeProgramme.Tables[DEGREEPROGRAMME_TABLE].Rows.Add(rowDegreeProgramme);
                    dsDegree.Tables[DEGREE_TABLE].Rows.Add(rowDegree);
                    break;
                case DBOperation.Edit:
                    rowDegree = FindDegreeRow(degreeProgramme);
                    if (rowDegree != null)
                    {
                        FillEDegreeRow(rowDegree, degreeProgramme);
                    }
                    else
                    {
                        throw new Exception("Degree not found in table");
                    }
                    break;
                case DBOperation.Delete:
                    rowDegreeProgramme = FindDegreeProgrammeRow(degreeProgramme);
                    rowDegree = FindDegreeRow(degreeProgramme);
                    if (rowDegreeProgramme != null && rowDegree != null)
                    {
                        rowDegree.Delete();
                        rowDegreeProgramme.Delete();
                        degreeProgrammes.Remove(degreeProgramme);
                    }
                    else
                    {
                        throw new Exception("Degree not found in table");
                    }
                    break;
            }
        }
        #endregion

        #region Build Parameters, Create Commands & Update database
        internal override bool UpdateAllSources()
        {
            dsMain = dsDegreeProgramme;
            bool check1 = UpdateDataSource(SQL_SELECT_DEGREEPROGRAMME, DEGREEPROGRAMME_TABLE, daDegreeProgramme);
            dsDegreeProgramme = dsMain;
            dsMain = dsDegree;
            bool check2 = UpdateDataSource(SQL_SELECT_DEGREE, DEGREE_TABLE, dsDegree);
            dsDegree = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            daDegreeProgramme.InsertCommand = new OleDbCommand("INSERT INTO DegreeProgramme (FacultyID, DegreeID) VALUES (@FacultyID, @DegreeID)", cnMain);
            daDegree.InsertCommand = new OleDbCommand("INSERT INTO Degree (DegreeID, Title, Description) VALUES (@DegreeID, @Title, @Description)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daDegreeProgramme.UpdateCommand = new OleDbCommand("UPDATE DegreeProgramme SET (@FacultyID, @DegreeID) WHERE FacultyID = @FacultyID AND DegreeID = @DegreeID", cnMain);
            daDegree.UpdateCommand = new OleDbCommand("UPDATE Degree SET (@DegreeID, @Title, @Description) WHERE DegreeID = @DegreeID", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daDegreeProgramme.DeleteCommand = new OleDbCommand("DELETE FROM DegreeProgramme WHERE FacultyID = @FacultyID AND DegreeID = @DegreeID", cnMain);
            daDegree.DeleteCommand = new OleDbCommand("DELETE FROM Degree WHERE DegreeID = @DegreeID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daDegreeProgramme.InsertCommand.Parameters.Add(param);

            SqlParameter param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daDegreeProgramme.InsertCommand.Parameters.Add(param);
            daDegree.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daDegree.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, -1, "Description");
            daDegree.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            daDegreeProgramme.InsertCommand.Parameters.Add(param);

            SqlParameter param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daDegreeProgramme.UpdateCommand.Parameters.Add(param);
            daDegree.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daDegree.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, -1, "Description");
            daDegree.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daDegreeProgramme.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daDegree.DeleteCommand.Parameters.Add(param);
            daDegreeProgramme.DeleteCommand.Parameters.Add(param);
        }S

        #endregion
    }
}
