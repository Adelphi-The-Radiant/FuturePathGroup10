using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FuturePathGroup10.Prerequisites;
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
        private const string SQL_SELECT_FACULTY_JOIN = "SELECT * FROM " +
            "(SELECT * FROM Faculty LEFT JOIN EnrollmentList ON Faculty.FacultyID = EnrollmentList.FacultyID ORDER BY Faculty.title) " +
            "AS FacultyJoined";
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
        public FacultyDB()
        {
            enrollments = new Collection<EnrollmentList>();
            faculties = new Collection<Faculty>();
            this.learnerDB = learnerDB;

            FillDataSet(SQL_SELECT_FACULTY, ENROLLMENTLIST_TABLE);
            dsEnrollmentList = dsMain;
            daEnrollmentList = daMain;

            FillDataSet(SQL_SELECT_FACULTY, FACULTY_TABLE);
            dsFaculty = dsMain;
            daFaculty = daMain;

            //dsMain and daMain remain for the joined table
            FillDataSet(SQL_SELECT_FACULTY_JOIN, JOINED_TABLE);

            LoadFaculties();
        }
        #endregion

        #region Load Faculties
        private void LoadFaculties()
        {
            int facultyID = -1;
            string candidateString;
            string title;
            string description;
            DateTime date;

            Faculty.Faculty faculty = null;
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
            row["LearnerID"] = enrollment.getLearner().getLearnerID();
            row["FacultyID"] = enrollment.getFaculty().getFacultyID();
            row["Date"] = enrollment.getDate();
        }

        private void FillFacultyRow(DataRow row, FacultyDB faculty)
        {
            row["FacultyID"] = faculty.FacultyID;
            row["Title"] = faculty.Title;
            row["Description"] = faculty.Description;

        }

        private DataRow FindEnrollmentListRow(EnrollmentList enrollment)
        {
            for (int i = 0; i < dsEnrollmentList.Tables[ENROLLMENTLIST_TABLE].Rows.Count; i++)
            {
                DataRow row = dsEnrollmentList.Tables[ENROLLMENTLIST_TABLE].Rows[i];
                //Two primary keys so we check both...
                if (Convert.ToInt32(row["LearnerID"]) == enrollment.getLearner().getLearnerID() && Convert.ToInt32(row["FacultyID"]) == enrollment.getFaculty().getFacultyID())
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindFacultyRow(Faculty faculty)
        {
            for (int i = 0; i < dsFaculty.Tables[FACULTY_TABLE].Rows.Count; i++)
            {
                DataRow row = dsFaculty.Tables[FACULTY_TABLE].Rows[i];
                if (Convert.ToInt32(row["FacultyID"]) == faculty.FacultyID)
                {
                    return row;
                }
            }
            return null;
        }

        private int findSEnrollmentListIndex(EnrollmentList enrollment)
        {
            for (int i = 0; i < enrollment.Count; i++)
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
                    rowEnrollment = dsEnrollmentList.Tables[ENROLLMENTLIST_TABLE].NewRow();
                    rowFaculty = dsSFaculty.Tables[FACULTY_TABLE].NewRow();
                    FillLEnrollmentListRow(rowEnrollment, enrollment);
                    FillFacultyRow(rowFaculty, enrollment.getFaculty());

                    dsEnrollmentList.Tables[ENROLLMENTLIST_TABLE].Rows.Add(rowEnrollment);
                    dsFaculty.Tables[FACULTY_TABLE].Rows.Add(rowFaculty);
                    break;
                case DBOperation.Edit:
                    rowEnrollment = FindEnrollmentListRow(enrollment);
                    if (rowEnrollment != null)
                    {
                        FillEnrollmentListRow(rowEnrollment, enrollment);
                    }
                    else
                    {
                        throw new Exception("Enrollment not found in table");
                    }
                    break;
                case DBOperation.Delete:
                    rowEnrollment = FindEnrollmentListRow(enrollment);
                    rowFaculty = FindFacultyRow(enrollment.getFaculty());
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
            bool check1 = UpdateDataSource(SQL_SELECT_ENROLLMENTLIST, ENROLLMENTLIST_TABLE, daEnrollmentList);
            dsEnrollmentList = dsMain;
            dsMain = dsFaculty;
            bool check2 = UpdateDataSource(SQL_SELECT_FACULTY, FACULTY_TABLE, dsFaculty);
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
            daSubject.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Result", SqlDbType.Float, 4, "Result");
            daLearnerSubject.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@SubjectDescription", SqlDbType.NVarChar, 255, "SubjectDescription");
            daSubject.UpdateCommand.Parameters.Add(param);
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

