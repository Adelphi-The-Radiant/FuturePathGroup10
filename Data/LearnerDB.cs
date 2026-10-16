using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using FuturePathGroup10.Business;

namespace FuturePathGroup10.Data
{
    //Used to fill up the Learners collection which contains learners from the DB. 
    //Obviously also allows editing of the DB through DataSetChange and UpdateDataSource TOGETHER
    public class LearnerDB : DB
    {
        #region Attributes
        public Collection<Learner> learners { get; private set; }
        
        private DataSet learnerDS;
        private DataSet gradeDS;

        private OleDbDataAdapter learnerDA;
        private OleDbDataAdapter gradeDA;
        private const string SQL_SELECT_GRADE_MEET = "SELECT * FROM Grade_Level WHERE LearnerID = ";
        private const string SQL_SELECT_GRADE = "SELECT * FROM Grade_Level";
        private const string SQL_SELECT_LEARNERS = "SELECT * FROM Learners";
        private const string TABLE_LEARNERS = "Learners";
        private const string TABLE_GRADE = "Grade_Level";
        #endregion


        public LearnerDB() : base()
        {
            learners = new Collection<Business.Learner>();
        }

        // Called by AdvisorController 
        public Collection<Business.Learner> GetAllLearners()
        {
            learners.Clear();                                // otherwise a second call adds duplicates
            FillDataSet(SQL_SELECT_LEARNERS, TABLE_LEARNERS);
            learnerDS = dsMain;
            learnerDA = daMain;
            FillDataSet(SQL_SELECT_GRADE, TABLE_GRADE);
            gradeDS = dsMain;
            gradeDA = daMain;
            LoadLearners();
            return learners;
        }
        #endregion

        #region Get earners
        //Get learner with a certain ID. Returns null if not found.
        public Learner getLearner(int learnerID)
        {
            foreach (Learner learner in learners)
            {
                if (learner.learnerID == learnerID)
                {
                    return learner;
                }
            }
            return null;
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
                    string grade11 = gradeRow["Grade11Mark"].ToString();
                    string grade12 = gradeRow["Grade12Mark"].ToString();
                    string phoneNumber = row["PhoneNumber"].ToString();
                    string fullname = row["FullName"].ToString();
                    string school = row["School"].ToString();
                    string email = row["Email"].ToString();
                    string province = row["Province"].ToString();

                    Learner learner = new Learner(Convert.ToInt32(learnerID), username, phoneNumber, fullname, school, grade11, grade12, email, province);
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
            FillDataSet(SQL_SELECT_GRADE_MEET + learnerID, TABLE_GRADE);
            DataSet GradeDSRow = dsMain;
   
            return GradeDSRow.Tables[TABLE_GRADE].Rows[0];
        }
        #endregion

        #region DataSet Updates
        //Helper methods to fill rows and find rows in the dataset for learners
        private void FillLearnerRow(DataRow row, Learner learner)
        {
            row["LearnerID"] = learner.learnerID;
            row["Username"] = learner.username;
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
            row["Grade11Mark"] = grade11Mark;
            row["Grade12Mark"] = grade12Mark;
        }
        //overloaded of previous
        private void FillGradeRow(DataRow row, Learner learner)
        {
            FillGradeRow(row, learner.learnerID, Convert.ToSingle(learner.overallGrade.grade11Result), Convert.ToSingle(learner.overallGrade.grade12Result));
        }
        //Helper method that finds the row in the dataset for a given learner. Returns null if not found.
        private DataRow FindLearnerRow(Learner learner)
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
        private DataRow FindGradeRow(Learner learner)
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

        private int findLearnerIndex(Learner learner)
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
        public void DataSetChange(Learner learner, DBOperation operation)
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
            bool check1 = UpdateDataSource(SQL_SELECT_LEARNERS,TABLE_LEARNERS, learnerDA);
            learnerDS = dsMain;
            dsMain = gradeDS;
            bool check2 = UpdateDataSource(SQL_SELECT_GRADE,TABLE_GRADE, gradeDA);
            gradeDS = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            learnerDA.InsertCommand = new OleDbCommand("INSERT into Learners (LearnerID, FullName, PhoneNumber, Email, Username, School, Province) VALUES (@LearnerID, @FullName, @PhoneNumber, @Email, @Username, @School, @Province)", cnMain);
            gradeDA.InsertCommand = new OleDbCommand("INSERT into Grade_Level (LearnerID, Grade11Mark, Grade12Mark) VALUES (@LearnerID, @Grade11Mark, @Grade12Mark)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            learnerDA.UpdateCommand = new OleDbCommand("UPDATE Learners SET (@LearnerID, @FullName, @PhoneNumber, @Email, @Username, @School, @Province) WHERE LearnerID = @LearnerID",cnMain);
            gradeDA.UpdateCommand = new OleDbCommand("UPDATE Grade_Level SET(@LearnerID, @Grade11Mark, @Grade12Mark) WHERE LearnerID = @LearnerID", cnMain);
            Build_UPDATE_Parameters();  
        }

        internal override void Create_DELETE_Command()
        {
            learnerDA.DeleteCommand = new OleDbCommand("DELETE FROM LEARNERS  WHERE LearnerID = @LearnerID", cnMain);
            gradeDA.DeleteCommand = new OleDbCommand("DELETE from Grade_Level  WHERE LearnerID = @LearnerID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            //LearnerDA setup
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 32, "ID");
            learnerDA.InsertCommand.Parameters.Add(param);
            gradeDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@FullName", SqlDbType.NVarChar, 255, "FullName");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 10, "PhoneNumber");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar, 255, "Email");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Username", SqlDbType.NVarChar, 255, "Username");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@School", SqlDbType.NVarChar, 255, "School");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Province", SqlDbType.NVarChar, 255, "Province");
            learnerDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Grade11Mark", SqlDbType.Float, 4, "Grade11Mark");
            gradeDA.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Grade12Mark", SqlDbType.Float, 4, "Grade12Mark");
            gradeDA.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            learnerDA.UpdateCommand.Parameters.Add(param);
            gradeDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@FullName", SqlDbType.NVarChar, 255, "FullName");
            learnerDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@PhoneNumber", SqlDbType.NVarChar, 10, "PhoneNumber");
            learnerDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Email", SqlDbType.NVarChar, 255, "Email");
            learnerDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Username", SqlDbType.NVarChar, 255, "Username");
            learnerDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@School", SqlDbType.NVarChar, 255, "School");
            learnerDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Province", SqlDbType.NVarChar, 255, "Province");
            learnerDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Grade11Mark", SqlDbType.Float, 4, "Grade11Mark");
            gradeDA.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Grade12Mark", SqlDbType.Float, 4, "Grade12Mark");
            gradeDA.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4,"LearnerID");
            learnerDA.DeleteCommand.Parameters.Add(param);
            gradeDA.DeleteCommand.Parameters.Add(param);
        }



        #endregion

        #region Simple single-learner methods (used by LearnerController)

        // Inserts ONE learner and returns the new AutoNumber ID.
        public int Add(Business.Learner learner)
        {
            using (OleDbConnection cn = new OleDbConnection(cnMain.ConnectionString))
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand(
                    "INSERT INTO Learner (FullName, PhoneNumber, Email, Username, School, Province) VALUES ( ?, ?, ?, ?, ?, ?)", cn);
   
                cmd.Parameters.AddWithValue("@FullName", learner.fullname ?? "");
                cmd.Parameters.AddWithValue("@PhoneNumber", learner.phoneNumber ?? "");
                cmd.Parameters.AddWithValue("@Email", learner.email ?? "");
                cmd.Parameters.AddWithValue("@Username", learner.username ?? "");
                cmd.Parameters.AddWithValue("@School", learner.school ?? "");
                cmd.Parameters.AddWithValue("@Province", learner.province ?? "");
                cmd.ExecuteNonQuery();


                return Convert.ToInt32(new OleDbCommand("SELECT @@IDENTITY", cn).ExecuteScalar());
            }
        }

        // Returns one learner, or null if not found.
        public Business.Learner GetById(int learnerId)
        {
            using (OleDbConnection cn = new OleDbConnection(cnMain.ConnectionString))
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM Learner WHERE LearnerID = ?", cn);
                cmd.Parameters.AddWithValue("@LearnerID", learnerId);

                string username, phone, fullname, school, email, province;
                using (OleDbDataReader rdr = cmd.ExecuteReader())
                {
                    if (!rdr.Read()) return null;
                    username = rdr["Username"].ToString();
                    phone = rdr["PhoneNumber"].ToString();
                    fullname = rdr["FullName"].ToString();
                    school = rdr["School"].ToString();
                    email = rdr["Email"].ToString();
                    province = rdr["Province"].ToString();
                }

                // grade marks live in their own table; a learner may not have a row yet
                string grade11 = "", grade12 = "";
                OleDbCommand gcmd = new OleDbCommand("SELECT * FROM GradeLevel WHERE LearnerID = ?", cn);
                gcmd.Parameters.AddWithValue("@LearnerID", learnerId);
                using (OleDbDataReader grd = gcmd.ExecuteReader())
                {
                    if (grd.Read())
                    {
                        grade11 = grd["Grade11Mark"].ToString();
                        grade12 = grd["Grade12Mark"].ToString();
                    }
                }

                return new Business.Learner(learnerId, username, phone, fullname, school, grade11, grade12, email, province);
            }
        }

        // Updates ONE learner's profile row.
        public void Update(Business.Learner learner)
        {
            using (OleDbConnection cn = new OleDbConnection(cnMain.ConnectionString))
            {
                cn.Open();
                OleDbCommand cmd = new OleDbCommand(
                    "UPDATE Learner SET FullName = ?, PhoneNumber = ?, Email = ?, Username = ?, School = ?, Province = ? WHERE LearnerID = ?", cn);
                cmd.Parameters.AddWithValue("@FullName", learner.fullname ?? "");
                cmd.Parameters.AddWithValue("@PhoneNumber", learner.phoneNumber ?? "");
                cmd.Parameters.AddWithValue("@Email", learner.email ?? "");
                cmd.Parameters.AddWithValue("@Username", learner.username ?? "");
                cmd.Parameters.AddWithValue("@School", learner.school ?? "");
                cmd.Parameters.AddWithValue("@Province", learner.province ?? "");
                cmd.Parameters.AddWithValue("@LearnerID", learner.learnerID);   // WHERE parameter goes LAST
                cmd.ExecuteNonQuery();
            }
        }

        #endregion



    }
}
