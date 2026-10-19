using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Data
{
    //Used to fill up the Learners collection which contains learners from the DB. 
    //Obviously also allows editing of the DB through DataSetChange and UpdateDataSource TOGETHER
    public class LearnerDB : DB
    {
        #region Attributes
        public Collection<Learner.Learner> learners { get; private set; }

        private DataSet learnerDS;
        private DataSet gradeDS;

        private OleDbDataAdapter learnerDA;
        private OleDbDataAdapter gradeDA;
        private const string SQL_SELECT_GRADE_MEET = "SELECT * FROM GradeLevel WHERE LearnerID = ";
        private const string SQL_SELECT_GRADE = "SELECT * FROM GradeLevel";
        private const string SQL_SELECT_LEARNERS = "SELECT * FROM Learner";
        private const string TABLE_LEARNERS = "Learner";
        private const string TABLE_GRADE = "GradeLevel";
        #endregion

        #region Constructor
        public LearnerDB() : base()
        {
            learners = new Collection<Learner.Learner>();
            FillDataSet(SQL_SELECT_LEARNERS, TABLE_LEARNERS);
            learnerDS = dsMain;
            learnerDA = daMain;
            FillDataSet(SQL_SELECT_GRADE, TABLE_GRADE);
            gradeDS = dsMain;
            gradeDA = daMain;
            LoadLearners();
        }
        #endregion

        #region Get learners
        //Get learner with a certain ID. Returns null if not found.
        public Learner.Learner getLearner(int learnerID)
        {
            foreach (Learner.Learner learner in learners)
            {
                if (learner.learnerID == learnerID)
                {
                    return learner;
                }
            }
            return null;
        }

        public int getNextID()
        {
            int highest = 0;
            foreach (Learner.Learner learner in learners)
            {
                if (learner.learnerID > highest)
                {
                    highest = learner.learnerID;
                }
            }
            return highest + 1;
        }

        #endregion

        #region Load Collection of Learners
        //Fill the learners collection from the dataset. done during initialisation.
        private void LoadLearners()
        {
            //New dataset because we'll need to load multiple datasets
            foreach (DataRow row in learnerDS.Tables[TABLE_LEARNERS].Rows)
            {
                try
                {
                    string learnerID = row["LearnerID"].ToString();
                    string username = row["Username"].ToString();
                    DataRow gradeRow = getGrades(Convert.ToInt32(learnerID));
                    string grade11 = gradeRow?["Grade11Result"].ToString() ?? "";
                    string grade12 = gradeRow?["Grade12Result"].ToString() ?? "";
                    string phoneNumber = row["PhoneNumber"].ToString();
                    string fullname = row["FullName"].ToString();
                    string school = row["School"].ToString();
                    string email = row["Email"].ToString();
                    string province = row["Province"].ToString();

                    Learner.Learner learner = new Learner.Learner(Convert.ToInt32(learnerID), username, phoneNumber, fullname, school, grade11, grade12, email, province);
                    learners.Add(learner);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error loading learner: " + e.Message);
                    continue;
                }
            }
        }

        //Ensure that GradeLevel is a 1 to 1 relation. If its not this code will just take the first entry and take the average.
        private DataRow getGrades(int learnerID)
        {
            foreach (DataRow row in gradeDS.Tables[TABLE_GRADE].Rows)
            {
                if (row.RowState != DataRowState.Deleted && Convert.ToInt32(row["LearnerID"]) == learnerID)
                {
                    return row;
                }
            }
            return null;
        }
        #endregion

        #region DataSet Updates
        //Helper methods to fill rows and find rows in the dataset for learners
        private void FillLearnerRow(DataRow row, Learner.Learner learner)
        {
            row["LearnerID"] = learner.learnerID;
            row["Username"] = learner.Username;
            row["PhoneNumber"] = learner.phoneNumber;
            row["FullName"] = learner.fullname;
            row["School"] = learner.school;
            row["Email"] = learner.email;
            row["Province"] = learner.province;
        }
        //Helper methods to fill rows and find rows in the dataset for grade level
        private void FillGradeRow(DataRow row, int learnerID, float grade11Mark, float grade12Mark)
        {
            row["LearnerID"] = learnerID;
            row["Grade11Result"] = grade11Mark;
            row["Grade12Result"] = grade12Mark;
        }
        //overloaded of previous
        private void FillGradeRow(DataRow row, Learner.Learner learner)
        {
            FillGradeRow(row, learner.learnerID, Convert.ToSingle(learner.overallGrade11), Convert.ToSingle(learner.overallGrade12));
        }
        //Helper method that finds the row in the dataset for a given learner. Returns null if not found.
        private DataRow FindLearnerRow(Learner.Learner learner)
        {
            for (int i = 0; i < learnerDS.Tables[TABLE_LEARNERS].Rows.Count; i++)
            {
                if (Convert.ToInt32(learnerDS.Tables[TABLE_LEARNERS].Rows[i]["LearnerID"]) == learner.learnerID)
                {
                    return learnerDS.Tables[TABLE_LEARNERS].Rows[i];
                }
            }
            return null;
        }
        //Helper method that finds the row in the dataset for a grade level. Returns null if not found.
        private DataRow FindGradeRow(Learner.Learner learner)
        {
            for (int i = 0; i < gradeDS.Tables[TABLE_GRADE].Rows.Count; i++)
            {
                if (Convert.ToInt32(gradeDS.Tables[TABLE_GRADE].Rows[i]["LearnerID"]) == learner.learnerID)
                {
                    return gradeDS.Tables[TABLE_GRADE].Rows[i];
                }
            }
            return null;
        }

        private int FindLearnerIndex(Learner.Learner learner)
        {
            for (int i = 0; i < learners.Count; i++)
            {
                if (learners[i].Equals(learner))
                {
                    return i;
                }
            }
            return -1;
        }
        //Modifies the dataset based on the operation given to it using a learner object. DOES NOT UPDATE THE ACTUAL DATABASE. Use UpdateDataSource() to update the database.
        //Throws an exception if the learner couldn't be found in edits and deletes. Handle appropriately.
        public void DataSetChange(Learner.Learner learner, DBOperation operation)
        {
            DataRow learnerRow;
            DataRow gradeRow;

            switch (operation)
            {
                case DBOperation.Add:
                    learnerRow = learnerDS.Tables[TABLE_LEARNERS].NewRow();
                    FillLearnerRow(learnerRow, learner);
                    learnerDS.Tables[TABLE_LEARNERS].Rows.Add(learnerRow);

                    gradeRow = gradeDS.Tables[TABLE_GRADE].NewRow();
                    FillGradeRow(gradeRow, learner);
                    gradeDS.Tables[TABLE_GRADE].Rows.Add(gradeRow);
                    learners.Add(learner);
                    break;

                case DBOperation.Edit:
                    learnerRow = FindLearnerRow(learner);
                    gradeRow = FindGradeRow(learner);
                    if (learnerRow != null && gradeRow != null)
                    {
                        FillLearnerRow(learnerRow, learner);
                        FillGradeRow(gradeRow, learner);
                    }
                    else
                    {
                        throw new Exception("Learner not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    learnerRow = FindLearnerRow(learner);
                    gradeRow = FindGradeRow(learner);
                    if (learnerRow != null && gradeRow != null)
                    {
                        learnerRow.Delete();
                        gradeRow.Delete();
                        learners.Remove(learner);
                    }
                    else
                    {
                        throw new Exception("Learner not found in Tables");
                    }
                    break;
            }
        }
        #endregion

        #region Build Parameters, Create Commands & Update database
        internal override bool UpdateAllSources()
        {
            dsMain = learnerDS;
            bool check1 = UpdateDataSource(SQL_SELECT_LEARNERS, TABLE_LEARNERS, learnerDA);
            learnerDS = dsMain;
            dsMain = gradeDS;
            bool check2 = UpdateDataSource(SQL_SELECT_GRADE, TABLE_GRADE, gradeDA);
            gradeDS = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            learnerDA.InsertCommand = new OleDbCommand("INSERT INTO Learner (LearnerID, FullName, PhoneNumber, Email, Username, School, Province) VALUES (?, ?, ?, ?, ?, ?, ?)", cnMain);
            gradeDA.InsertCommand = new OleDbCommand("INSERT INTO GradeLevel (LearnerID, Grade11Result, Grade12Result) VALUES (?, ?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            learnerDA.UpdateCommand = new OleDbCommand("UPDATE Learner SET FullName = ?, PhoneNumber = ?, Email = ?, Username = ?, School = ?, Province = ? WHERE LearnerID = ?", cnMain);
            gradeDA.UpdateCommand = new OleDbCommand("UPDATE GradeLevel SET Grade11Result = ?, Grade12Result = ? WHERE LearnerID = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            learnerDA.DeleteCommand = new OleDbCommand("DELETE FROM Learner WHERE LearnerID = ?", cnMain);
            gradeDA.DeleteCommand = new OleDbCommand("DELETE FROM GradeLevel WHERE LearnerID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        private static OleDbParameter Param(string name, OleDbType type, int size, string sourceColumn)
        {
            return new OleDbParameter(name, type, size, sourceColumn);
        }

        private void AddLearnerParams(OleDbCommand command, bool idLast)
        {
            if (!idLast) command.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            command.Parameters.Add(Param("@FullName", OleDbType.VarWChar, 255, "FullName"));
            command.Parameters.Add(Param("@PhoneNumber", OleDbType.VarWChar, 255, "PhoneNumber"));
            command.Parameters.Add(Param("@Email", OleDbType.VarWChar, 255, "Email"));
            command.Parameters.Add(Param("@Username", OleDbType.VarWChar, 255, "Username"));
            command.Parameters.Add(Param("@School", OleDbType.VarWChar, 255, "School"));
            command.Parameters.Add(Param("@Province", OleDbType.VarWChar, 255, "Province"));
            if (idLast) command.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
        }

        private void AddGradeParams(OleDbCommand command, bool idLast)
        {
            if (!idLast) command.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            command.Parameters.Add(Param("@Grade11Result", OleDbType.Single, 4, "Grade11Result"));
            command.Parameters.Add(Param("@Grade12Result", OleDbType.Single, 4, "Grade12Result"));
            if (idLast) command.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
        }

        internal override void Build_INSERT_Parameters()
        {
            //LearnerDA setup
            AddLearnerParams(learnerDA.InsertCommand, false);
            AddGradeParams(gradeDA.InsertCommand, false);
        }

        internal override void Build_UPDATE_Parameters()
        {
            AddLearnerParams(learnerDA.UpdateCommand, true);
            AddGradeParams(gradeDA.UpdateCommand, true);
        }

        internal override void Build_DELETE_Parameters()
        {
            learnerDA.DeleteCommand.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            gradeDA.DeleteCommand.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
        }

        #endregion


    }
}
