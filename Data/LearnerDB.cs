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
            throw new NotImplementedException();
        }

        private void Create_INSERT_Command()
        {
            learnerDA.InsertCommand = new OleDbCommand("INSERT into Learners (LearnerID, FullName, PhoneNumber, Email, Username, School, Province) VALUES (@LearnerID, @FullName, @PhoneNumber, @Email, @Username, @School, @Province)", cnMain);
            gradeDA.InsertCommand = new OleDbCommand("INSERT into Grade_Level (LearnerID, Grade11Mark, Grade12Mark) VALUES (@LearnerID, @Grade11Mark, @Grade12Mark)", cnMain);
            Build_INSERT_Parameters();
        }

        public bool UpdateDataSource()
        {
            throw new NotImplementedException();
        }

        private void Build_UPDATE_Parameters()
        {
            throw new NotImplementedException();
        }

        private void Create_UPDATE_Command()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
