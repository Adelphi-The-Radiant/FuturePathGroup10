using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using FuturePathGroup10.Learner;

namespace FuturePathGroup10.Data
{
    public class LearnerDB : DB
    {

        private Collection<Learner.Learner> learners {get;}
        private DataSet learnerDS;
        private DataSet gradeDS;

        private OleDbDataAdapter learnerDA;
        private OleDbDataAdapter gradeDA;
        private const string SQL_SELECT_GRADE_MEET = "SELECT * FROM Grade_Level WHERE LearnerID = ";
        private const string SQL_SELECT_GRADE = "SELECT * FROM Grade_Level";
        private const string SQL_SELECT_LEARNERS = "SELECT * FROM Learners";
        private const string TABLE_LEARNERS = "Learners";
        private const string TABLE_GRADE = "Grade_Level";


        LearnerDB() : base()
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

        #region Load Collection of Learners
        public void LoadLearners()
        {
            
            //New dataset because we'll need to load multiple datasets
            foreach (DataRow row in learnerDS[TABLE_LEARNERS].Rows)
            {
                
                string learnerID = row["LearnerID"];
                string username = row["Username"];
                DataRow gradeRow = getGrades(learnerID);
                string grade11 = gradeRow["Grade11Mark"].ToString();
                string grade12 = gradeRow["Grade12Mark"].ToString();
                string phoneNumber = row["PhoneNumber"];
                string fullname = row["FullName"];
                string school = row["School"];
                string email = row["Email"];
                string province = row["Province"];

                Learner.Learner learner = new Learner.Learner(Convert.ToInt32(learnerID), username, phoneNumber, fullname, school, grade11, grade12, email, province);
                learners.Add(learner);
            }
        }

        //Ensure that GradeLevel is a 1 to 1 relation. If its not this code will just take the first entry and take the average.
        private DataRow getGrades(int learnerID)
        {
            FillDataSet(SQL_SELECT_GRADE_MEET + learnerID, TABLE_GRADE);
            DataSet GradeDSRow = dsMain;
   
            return GradeDSRow.Tables[TABLE_GRADE].Rows[0];
        }
        #endregion

        #region DataSet Updates
        private void FillLearnerRow(DataRow row, Learner.Learner learner)
        {
            row["LearnerID"] = learner.learnerID;
            row["Username"] = learner.username;
            row["PhoneNumber"] = learner.phoneNumber;
            row["FullName"] = learner.fullname;
            row["School"] = learner.school;
            row["Email"] = learner.email;
            row["Province"] = learner.province;
        }

        private void FillGradeRow(DataRow row, int learnerID, float grade11Mark, float grade12Mark)
        {
            row["LearnerID"] = learnerID;
            row["Grade11Mark"] = grade11Mark;
            row["Grade12Mark"] = grade12Mark;
        }

        private void FillGradeRow(DataRow row, Learner.Learner learner)
        {
            FillGradeRow(row, learner.learnerID, Convert.ToSingle(learner.grade11), Convert.ToSingle(learner.grade12));
        }

        private DataRow FindLearnerRow(Learner.Learner learner)
        {
          
            for (int i = 0; i < LearnerDS.Tables[TABLE_LEARNERS].Rows.Count; i++)
            {
                if (Convert.ToInt32(LearnerDS.Tables[TABLE_LEARNERS].Rows[i]["LearnerID"]) == learner.learnerID)
                {
                    return LearnerDS.Tables[TABLE_LEARNERS].Rows[i];
                }
            }
            return null;
        }

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

        //Internal?
        internal void DataSetChange(Learner.Learner learner, DBOperation operation)
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
                    FillGradeRow(gradeRow,learner);
                    gradeDS.Tables[TABLE_GRADE].Rows.Add(gradeRow);
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
                    }
                    else
                    {
                        throw new Exception("Learner not found in Tables");
                    }
            }
        }
        #endregion

        #region Build Parameters, Create Commands & Update database
        private void Build_INSERT_Parameters()
        {
            //LearnerDA setup
            SQLParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 32,"ID");
            learnerDA.InsertCommand.Parameters.Add(param);
            gradeDA.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@FullName", SqlDbType.NVarChar, 255,"FullName");
            learnerDA.InsertCommand.Paramets.Add(param);
            
            param = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 10,"PhoneNumber");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar, 255, "Email");
            learnerDA.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Username", SqlDbType.NVarChar, 255,"Username");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@School", SqlDbType.NVarChar, 255, "School");
            learnerDA.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Province", SqlDbType.NVarChar, 255,"Province");
            learnerDA.InsertCommand.Parameters.Add(param);
            
            //GradeDA setup
            param = new SqlParameter("@Grade11Mark", SqlDbType.Float, 10, "Grade11Mark");
            gradeDA.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Grade12Mark", SqlDbType.Float, 10, "Grade12Mark");
            gradeDA.InsertCommand.Parameters.Add(param);
        }

        private void Create_INSERT_Command()
        {
            learnerDA.InsertCommand = new OleDbCommand("INSERT into Learners (LearnerID, FullName, PhoneNumber, Email, Username, School, Province) VALUES (@LearnerID, @FullName, @PhoneNumber, @Email, @Username, @School, @Province)", cnMain);
            gradeDA.InsertCommand = new OleDbCommand("INSERT into Grade_Level (LearnerID, Grade11Mark, Grade12Mark) VALUES (@LearnerID, @Grade11Mark, @Grade12Mark)", cnMain);
            Build_INSERT_Parameters();
        }

        public bool UpdateDataSource(DBOperation operation)
        {
            switch (operation)
            {
                case DBOperation.Add:
                    Create_INSERT_Command();
                    break;
                case DBOperation.Edit:
                    Create_UPDATE_Command();
                    break;
                case DBOperation.Delete:
                   Create_DELETE_Command();
                    break;
            }
            return UpdateAllSources();
        }

        private bool UpdateAllSources()
        {
            bool check1 = UpdateDataSource(SQL_SELECT_LEARNERS,TABLE_LEARNERS, learnerDA);
            learnerDS = dsMain;
            bool check2 = UpdateDataSource(SQL_SELECT_GRADE,TABLE_GRADE, gradeDA);
            gradeDS = dsMain;
            return check1 && check2;
        }

        private void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 32,"LearnerID");
            learnerDA.UpdateCommand.Parameters.Add(param);
            gradeDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@FullName", SqlDbType.NVarChar, 255,"FullName");
            learnerDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 10,"PhoneNumber");
            learnerDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Email", SqlDbType.NVarChar, 255, "Email");
            learnerDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Username", SqlDbType.NVarChar, 255,"Username");
            learnerDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@School", SqlDbType.NVarChar, 255, "School");
            learnerDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Province", SqlDbType.NVarChar, 255,"Province");
            learnerDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Grade11Mark", SqlDbType.Float, 10, "Grade11Mark");
            gradeDA.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Grade12Mark", SqlDbType.Float, 10, "Grade12Mark");
            gradeDA.UpdateCommand.Parameters.Add(param);
        }

        private void Create_UPDATE_Command()
        {
            learnerDA.UpdateCommand = new OleDbCommand("UPDATE Learners SET (@LearnerID, @FullName, @PhoneNumber, @Email, @Username, @School, @Province) WHERE LearnerID = @LearnerID",cnMain);
            gradeDA.UpdateCommand = new OleDbCommand("UPDATE Grade_Level SET(@LearnerID, @Grade11Mark, @Grade12Mark) WHERE LearnerID = @LearnerID", cnMain);
            Build_UPDATE_Parameters();  
        }

        private void Create_DELETE_Command()
        {
            learnerDA.DeleteCommand = new OleDbCommand("DELETE FROM LEARNERS  WHERE LearnerID = @LearnerID", cnMain);
            gradeDA.DeleteCommand = new OleDbCommand("DELETE from Grade_Level  WHERE LearnerID = @LearnerID", cnMain);
            Build_DELETE_Parameters();
        }

        private void Build_DELETE_Parameters()
        {
            param = new SqlParameter("@LearnerID", SqlDbType.Int, 32,"LearnerID");
            learnerDA.DeleteCommand.Parameters.Add(param);
            gradeDA.DeleteCommand.Parameters.Add(param);
        }
            
        

        #endregion

    }
}
