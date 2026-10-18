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
            degrees = new Collection<Degree>();
            programmes = new Collection<DegreeProgramme>();
            this.facultyDB = facultyDB;

            FillDataSet(SQL_SELECT_DEGREEPROGRAMME, TABLE_DEGREEPROGRAMME);
            dsDegreeProgramme = dsMain;
            daDegreeProgramme = daMain;

            FillDataSet(SQL_SELECT_DEGREE, TABLE_DEGREE);
            dsDegree = dsMain;
            daDegree = daMain;

            //dsMain and daMain remain for the joined table
            FillDataSet(SQL_SELECT_DEGREE_JOIN, JOINED_TABLE);

            LoadDegreeProgrammes();
        }
        #endregion

        #region Getter

        public Degree getDegree(int degreeID)
        {
            foreach(Degree degree in degrees)
            {
                if (degree.degreeID == degreeID)
                {
                    return degree;
                }
            }
            return null;
        }

        #endregion

        #region Load Degree Programmes
        private void LoadDegreeProgrammes()
        {
            int degreeID = -1;
            int facultyID = -1;
            string title;
            string description;

            Degree degree = null;
            DegreeProgramme degreeProgramme;
            Faculty faculty = null;

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
                programmes.Add(degreeProgramme);
            }
        }
        #endregion

        #region Dataset Updates
        private void FillDegreeProgrammeRow(DataRow row, DegreeProgramme degreeProgramme)
        {
            row["FacultyID"] = degreeProgramme.faculty.facultyID;
            row["DegreeID"] = degreeProgramme.degree.degreeID;
        }

        private void FillDegreeRow(DataRow row, DegreeProgramme degreeProgramme)
        {
            row["DegreeID"] = degreeProgramme.degree.degreeID;
            row["Title"] = degreeProgramme.degree.title;
            row["Description"] = degreeProgramme.degree.description;

        }

        private DataRow FindDegreeProgrammeRow(DegreeProgramme degreeProgramme)
        {
            for (int i = 0; i < dsDegreeProgramme.Tables[TABLE_DEGREEPROGRAMME].Rows.Count; i++)
            {
                DataRow row = dsDegreeProgramme.Tables[TABLE_DEGREEPROGRAMME].Rows[i];
                if (Convert.ToInt32(row["FacultyID"]) == degreeProgramme.faculty.facultyID && Convert.ToInt32(row["DegreeID"]) == degreeProgramme.degree.degreeID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDegreeRow(Degree degree)
        {
            for (int i = 0; i < dsDegree.Tables[TABLE_DEGREE].Rows.Count; i++)
            {
                DataRow row = dsDegree.Tables[TABLE_DEGREE].Rows[i];
                if (Convert.ToInt32(row["DegreeID"]) == degree.degreeID)
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
                    rowDegreeProgramme = dsDegreeProgramme.Tables[TABLE_DEGREEPROGRAMME].NewRow();
                    rowDegree = dsDegree.Tables[TABLE_DEGREE].NewRow();
                    FillDegreeProgrammeRow(rowDegreeProgramme, degree);
                    FillDegreeRow(rowDegree, degree);

                    dsDegreeProgramme.Tables[TABLE_DEGREEPROGRAMME].Rows.Add(rowDegreeProgramme);
                    dsDegree.Tables[TABLE_DEGREE].Rows.Add(rowDegree);

                    programmes.Add(degree);
                    break;
                case DBOperation.Edit:
                    rowDegreeProgramme = FindDegreeProgrammeRow(degree);
                    rowDegree = FindDegreeRow(degree.degree);
                    if (rowDegree != null && rowDegreeProgramme != null)
                    {
                        FillDegreeRow(rowDegree, degree);
                        FillDegreeProgrammeRow(rowDegreeProgramme, degree);
                    }
                    else
                    {
                        throw new Exception("Degree not found in table");
                    }
                    break;
                case DBOperation.Delete:
                    rowDegreeProgramme = FindDegreeProgrammeRow(degree);
                    rowDegree = FindDegreeRow(degree.degree);
                    if (rowDegreeProgramme != null && rowDegree != null)
                    {
                        rowDegree.Delete();
                        rowDegreeProgramme.Delete();
                        programmes.Remove(degree);
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
            bool check1 = UpdateDataSource(SQL_SELECT_DEGREEPROGRAMME, TABLE_DEGREEPROGRAMME, daDegreeProgramme);
            dsDegreeProgramme = dsMain;
            dsMain = dsDegree;
            bool check2 = UpdateDataSource(SQL_SELECT_DEGREE, TABLE_DEGREE, daDegree);
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

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daDegreeProgramme.InsertCommand.Parameters.Add(param);
            daDegree.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daDegree.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, -1, "Description");
            daDegree.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daDegreeProgramme.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
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
        }

        #endregion
    }
}
