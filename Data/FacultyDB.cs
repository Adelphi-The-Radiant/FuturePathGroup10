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
    public class FacultyDB : DB
    {
        #region Attributes
        // joins Faculty with Enrollment table
        private const string SQL_SELECT_FACULTY_JOIN = "SELECT Faculty.FacultyID AS FacultyID, Faculty.Title AS Title, Faculty.Description AS Description, EnrollmentList.LearnerID AS LearnerID, EnrollmentList.EnrollmentDate AS [Date] " +
            "FROM Faculty LEFT JOIN EnrollmentList ON Faculty.FacultyID = EnrollmentList.FacultyID " +
            "ORDER BY Faculty.Title";
        private const string JOINED_TABLE = "FacultyJoined";

        private const string TABLE_FACULTY = "Faculty";
        private const string TABLE_ENROLLMENTLIST = "EnrollmentList";

        private const string SQL_SELECT_FACULTY = "SELECT * FROM Faculty";
        private const string SQL_SELECT_ENROLLMENTLIST = "SELECT * FROM EnrollmentList";

        public Collection<Faculty> faculties { get; private set; }
        public Collection<EnrollmentList> enrollments { get; private set; }

        public DataSet dsFaculty;
        public DataSet dsEnrollmentList;

        public OleDbDataAdapter daFaculty;
        public OleDbDataAdapter daEnrollmentList;

        private LearnerDB learnerDB;
        #endregion

        #region Constructor
        public FacultyDB(LearnerDB learnerDB) : base()
        {
            enrollments = new Collection<EnrollmentList>();
            faculties = new Collection<Faculty>();
            this.learnerDB = learnerDB;

            FillDataSet(SQL_SELECT_ENROLLMENTLIST, TABLE_ENROLLMENTLIST);
            dsEnrollmentList = dsMain;
            daEnrollmentList = daMain;

            FillDataSet(SQL_SELECT_FACULTY, TABLE_FACULTY);
            dsFaculty = dsMain;
            daFaculty = daMain;

            //dsMain and daMain remain for the joined table
            FillDataSet(SQL_SELECT_FACULTY_JOIN, JOINED_TABLE);

            LoadFaculties();
        }
        #endregion

        public Faculty getFaculty(int facultyID)
        {
            foreach(Faculty faculty in faculties)
            {
                if (faculty.facultyID == facultyID)
                {
                    return faculty;
                }
            }
            return null;
        }

        #region Load Faculties
        private void LoadFaculties()
        {
            // Fail-fast if the joined table wasn't created by FillDataSet
            if (dsMain == null || !dsMain.Tables.Contains(JOINED_TABLE))
                throw new InvalidOperationException($"Missing dataset table: {JOINED_TABLE}. Ensure FillDataSet succeeded and the SQL/schema are correct.");

            int facultyID = -1;
            string candidateString;
            string title;
            string description;
            DateTime date;

            Faculty faculty = null;
            EnrollmentList enrollmentList;
            Learner.Learner learner;

            foreach (DataRow row in dsMain.Tables[JOINED_TABLE].Rows)
            {
                int candidateID = Convert.ToInt32(row["FacultyID"]);

                //Should ALWAYS activate on the first loop unless the subjects name is Not A String
                if (candidateID != facultyID)
                {
                    facultyID = candidateID;
                    title = (string)row["Title"];
                    description = (string)row["Description"];
                    //Ensures we dont have a bunch of duplicate subjects as this is a many to one relation
                    faculty = new Faculty(facultyID, title, description);
                    faculties.Add(faculty);
                }

                // if a faculty has no enrollments yet
                if (row["LearnerID"] != DBNull.Value)
                {
                    learner = learnerDB.getLearner(Convert.ToInt32(row["LearnerID"]));
                    date = Convert.ToDateTime(row["Date"]);

                    enrollmentList = new EnrollmentList(learner, faculty, date);
                    enrollments.Add(enrollmentList);
                }
            }
        }
        #endregion

        #region Dataset Updates
        private void FillEnrollmentListRow(DataRow row, EnrollmentList enrollment)
        {
            row["LearnerID"] = enrollment.learner.learnerID;
            row["FacultyID"] = enrollment.faculty.facultyID;
            row["Date"] = enrollment.date;
        }

        private void FillFacultyRow(DataRow row, Faculty faculty)
        {
            row["FacultyID"] = faculty.facultyID;
            row["Title"] = faculty.title;
            row["Description"] = faculty.description;

        }

        private DataRow FindEnrollmentListRow(EnrollmentList enrollment)
        {
            for (int i = 0; i < dsEnrollmentList.Tables[TABLE_ENROLLMENTLIST].Rows.Count; i++)
            {
                DataRow row = dsEnrollmentList.Tables[TABLE_ENROLLMENTLIST].Rows[i];
                //Two primary keys so we check both...
                if (Convert.ToInt32(row["LearnerID"]) == enrollment.learner.learnerID && Convert.ToInt32(row["FacultyID"]) == enrollment.faculty.facultyID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindFacultyRow(Faculty faculty)
        {
            for (int i = 0; i < dsFaculty.Tables[TABLE_FACULTY].Rows.Count; i++)
            {
                DataRow row = dsFaculty.Tables[TABLE_FACULTY].Rows[i];
                if (Convert.ToInt32(row["FacultyID"]) == faculty.facultyID)
                {
                    return row;
                }
            }
            return null;
        }

        private int findEnrollmentListIndex(EnrollmentList enrollment)
        {
            for (int i = 0; i < enrollments.Count; i++)
            {
                if (enrollments[i].Equals(enrollment))
                {
                    return i;
                }
            }
            return -1;
        }

        public void DataSetChange(EnrollmentList enrollment, DBOperation operation)
        {
            DataRow rowEnrollment;
            DataRow rowFaculty;
            switch (operation)
            {
                case DBOperation.Add:
                    rowEnrollment = dsEnrollmentList.Tables[TABLE_ENROLLMENTLIST].NewRow();
                    rowFaculty = dsFaculty.Tables[TABLE_FACULTY].NewRow();
                    FillEnrollmentListRow(rowEnrollment, enrollment);
                    FillFacultyRow(rowFaculty, enrollment.faculty);

                    dsEnrollmentList.Tables[TABLE_ENROLLMENTLIST].Rows.Add(rowEnrollment);
                    dsFaculty.Tables[TABLE_FACULTY].Rows.Add(rowFaculty);
                    break;
                case DBOperation.Edit:
                    rowEnrollment = FindEnrollmentListRow(enrollment);
                    rowFaculty = FindFacultyRow(enrollment.faculty);
                    if (rowEnrollment != null && rowFaculty != null)
                    {
                        FillEnrollmentListRow(rowEnrollment, enrollment);
                        FillFacultyRow(rowFaculty, enrollment.faculty);
                    }
                    else
                    {
                        throw new Exception("Enrollment not found in table");
                    }
                    break;
                case DBOperation.Delete:
                    rowEnrollment = FindEnrollmentListRow(enrollment);
                    rowFaculty = FindFacultyRow(enrollment.faculty);
                    if (rowEnrollment != null && rowFaculty != null)
                    {
                        rowEnrollment.Delete();
                        rowFaculty.Delete();
                        enrollments.Remove(enrollment);
                    }
                    else
                    {
                        throw new Exception("Enrollment not found in table");
                    }
                    break;
            }
        }
        #endregion

        #region Build Parameters, Create Commands & Update database
        internal override bool UpdateAllSources()
        {
            dsMain = dsEnrollmentList;
            bool check1 = UpdateDataSource(SQL_SELECT_ENROLLMENTLIST, TABLE_ENROLLMENTLIST, daEnrollmentList);
            dsEnrollmentList = dsMain;
            dsMain = dsFaculty;
            bool check2 = UpdateDataSource(SQL_SELECT_FACULTY, TABLE_FACULTY, daFaculty);
            dsFaculty = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            daEnrollmentList.InsertCommand = new OleDbCommand("INSERT INTO EnrollmentList (LearnerID, FacultyID, Date) VALUES (@LearnerID, @FacultyID, @Date)", cnMain);
            daFaculty.InsertCommand = new OleDbCommand("INSERT INTO Faculty (FacultyID, Title, Description) VALUES (@FacultyID, @Title, @Description)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daEnrollmentList.UpdateCommand = new OleDbCommand("UPDATE EnrollmentList SET (@LearnerID, @FacultyID, @Date) WHERE LearnerID = @LearnerID AND FacultyID = @FacultyID", cnMain);
            daFaculty.UpdateCommand = new OleDbCommand("UPDATE Faculty SET ( @Title, @Description) WHERE FacultyID = @FacultyID", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daEnrollmentList.DeleteCommand = new OleDbCommand("DELETE FROM EnrollmentLIst WHERE LearnerID = @LearnerID AND FacultyID = @FacultyID", cnMain);
            daFaculty.DeleteCommand = new OleDbCommand("DELETE FROM Faculty WHERE FacultyID = @FacultyID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daEnrollmentList.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daEnrollmentList.InsertCommand.Parameters.Add(param);
            daFaculty.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.Date, 8, "Date");
            daFaculty.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daFaculty.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, -1, "Description");
            daFaculty.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daEnrollmentList.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daEnrollmentList.UpdateCommand.Parameters.Add(param);
            daFaculty.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Date", SqlDbType.Date, 8, "Date");
            daFaculty.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daFaculty.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, -1, "Description");
            daFaculty.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daEnrollmentList.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daEnrollmentList.DeleteCommand.Parameters.Add(param);
            daFaculty.DeleteCommand.Parameters.Add(param);
        }

        #endregion

    }
}

