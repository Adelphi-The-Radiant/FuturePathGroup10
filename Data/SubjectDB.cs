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
    //Used to get access to learnerSubject instances from the DB accessed via the learnerSubjects collection.
    //This class also fills up the learner Collection from LearnerDB which is required.
    //Obviously also allows editing of the DB through DataSetChange and UpdateDataSource TOGETHER
    public class SubjectDB : DB
    {
        #region Attributes
        private const string SQL_SELECT_SUBJECT_JOIN = "SELECT * FROM " +
            "(SELECT * FROM Subject LEFT JOIN LearnerSubject ON Subject.SubjectName = LearnerSubject.SubjectName ORDER BY Subject.SubjectName) " +
            "AS SubjectJoined";
        private const string JOINED_TABLE = "SubjectJoined";

        private const string SQL_SELECT_SUBJECT = "SELECT * FROM Subject";
        private const string SQL_SELECT_LEARNERSUBJECT = "SELECT * FROM LearnerSubject";

        private const string SUBJECT_TABLE = "Subject";
        private const string LEARNERSUBJECT_TABLE = "LearnerSubject";

        private DataSet dsLearnerSubject;
        private DataSet dsSubject;
        
        private OleDbDataAdapter daLearnerSubject;
        private OleDbDataAdapter daSubject;

        public Collection<LearnerSubject> learnerSubjects { get; private set; }
        public Collection<Subject> subjects { get; private set; }
        private LearnerDB learnerDB;
        #endregion
        #region Constructor
        //Requires a learnerDB to be passed so that there is consistency between objects
        public SubjectDB(LearnerDB learnerDB) : base()
        {
            learnerSubjects = new Collection<LearnerSubject>();
            subjects = new Collection<Subject>();
            this.learnerDB = learnerDB;
            
            FillDataSet(SQL_SELECT_LEARNERSUBJECT, LEARNERSUBJECT_TABLE);
            dsLearnerSubject = dsMain;
            daLearnerSubject = daMain;
            
            FillDataSet(SQL_SELECT_SUBJECT, SUBJECT_TABLE);
            dsLearnerSubject = dsMain;
            daLearnerSubject = daMain;
            
            //dsMain and daMain remain for the joined table. I will probably have to change this later.
            FillDataSet(SQL_SELECT_SUBJECT_JOIN, JOINED_TABLE);

            LoadLearnerSubjects();
        }
        #endregion
        #region Load Learner Subjects
        //This also fills up our learner object with all their subjects.
        //Also this has O(n^2) complexity because of how it finds learners LOL
        private void LoadLearnerSubjects()
        {
            string subjectName = "Not A String";
            string candidateString;
            string subjectDescription;
            float  result;
            
            Subject subject = null;
            LearnerSubject learnerSubject;
            Learner.Learner learner;

            foreach (DataRow row in dsMain.Tables[JOINED_TABLE].Rows)
            {
                candidateString = row["SubjectName"].ToString();

                //Should ALWAYS activate on the first loop unless the subjects name is Not A String. Also on the assumption that subjectName is actually a primary key
                if (candidateString != subjectName)
                {
                    subjectName = candidateString;
                    subjectDescription = (string)row["SubjectDescription"];
                    //Ensures we dont have a bunch of duplicate subjects as this is a many to one relation. This method works because the rows will be ordered by subjectName.
                    subject = new Subject(subjectName, subjectDescription);
                    subjects.Add(subject);
                }

                result = Convert.ToSingle(row["Result"]);

                learner = learnerDB.getLearner(Convert.ToInt32(row["LearnerID"]));

                learnerSubject = new LearnerSubject(learner, subject, result);

                learnerSubjects.Add(learnerSubject);
                learner.addLearnerSubject(learnerSubject);
            }
            
        }
        #endregion

        #region Dataset Updates
        private void fillLearnerSubjectRow(DataRow row, LearnerSubject learnerSubject)
        {
            row["LearnerID"] = learnerSubject.learner.learnerID;
            row["SubjectName"] = learnerSubject.subject.subjectName;
            row["Result"] = learnerSubject.result;
        }

        private void fillSubjectRow(DataRow row, LearnerSubject learnerSubject)
        {
            row["SubjectName"] = learnerSubject.subject.subjectName;
            row["SubjectDescription"] = learnerSubject.subject.subjectDescription;
        }

        private DataRow findLearnerSubjectRow(LearnerSubject learnerSubject)
        {
            for (int i = 0; i < dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].Rows.Count; i++)
            {
                DataRow row = dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].Rows[i];
                //Two primary keys so we check both...
                if (row["LearnerID"].ToString() == learnerSubject.learner.learnerID.ToString())
                {
                    return row;
                }
            }
            return null;
        }
        
        private DataRow findSubjectRow(LearnerSubject learnerSubject)
        {
            for (int i = 0; i < dsSubject.Tables[SUBJECT_TABLE].Rows.Count; i++)
            {
                DataRow row = dsSubject.Tables[SUBJECT_TABLE].Rows[i];
                //Two primary keys so we check both...
                if (row["SubjectName"].ToString() == learnerSubject.subject.subjectName)
                {
                    return row;
                }
            }
            return null;
        }

        private int findSubjectIndex(LearnerSubject learnerSubject)
        {
            for (int i = 0; i < learnerSubjects.Count; i++)
            {
                if (learnerSubjects[i].Equals(learnerSubject))
                {
                    return i;
                }
            }
            return -1;
        }

        public void DataSetChange(LearnerSubject learnerSubject, DBOperation operation)
        {
            DataRow rowLearnerSubject;
            DataRow rowSubject;
            switch (operation)
            {
                case DBOperation.Add:
                    rowLearnerSubject = dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].NewRow();
                    rowSubject = dsSubject.Tables[SUBJECT_TABLE].NewRow();
                    fillLearnerSubjectRow(rowLearnerSubject, learnerSubject);
                    fillLearnerSubjectRow(rowSubject, learnerSubject);
                    
                    dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].Rows.Add(rowLearnerSubject);
                    dsSubject.Tables[SUBJECT_TABLE].Rows.Add(rowSubject);
                    break;
                case DBOperation.Edit:
                    rowLearnerSubject = findLearnerSubjectRow(learnerSubject);
                    if (rowLearnerSubject != null)
                    {
                        fillLearnerSubjectRow(rowLearnerSubject, learnerSubject);
                    }
                    else
                    {
                        throw new Exception("LearnerSubject not found in table");
                    }
                    break;
                case DBOperation.Delete:
                    rowLearnerSubject = findLearnerSubjectRow(learnerSubject);
                    rowSubject = findSubjectRow(learnerSubject);
                    if (rowSubject != null && rowLearnerSubject != null)
                    {
                        rowLearnerSubject.Delete();
                        rowSubject.Delete();
                        learnerSubjects.Remove(learnerSubject);
                    }
                    else
                    {
                        throw new Exception("LearnerSubject not found in table");
                    }
                    break;
            }
        }

        #endregion

        #region Build Parameters, Create Commands & Update database
        internal override bool UpdateAllSources()
        {
            dsMain = dsLearnerSubject;
            bool check1 = UpdateDataSource(SQL_SELECT_LEARNERSUBJECT, LEARNERSUBJECT_TABLE, daLearnerSubject);
            dsLearnerSubject = dsMain;
            dsMain = dsSubject;
            bool check2 = UpdateDataSource(SQL_SELECT_SUBJECT, SUBJECT_TABLE, daSubject);
            dsSubject = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            daLearnerSubject.InsertCommand = new OleDbCommand("INSERT INTO LearnerSubject (LearnerID, SubjectName, Result) VALUES (@LearnerID, @SubjectName, @Result)", cnMain);
            daSubject.InsertCommand = new OleDbCommand("INSERT INTO Subject (SubjectName, SubjectDescription) VALUES (@SubjectName, @SubjectDescription)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daLearnerSubject.UpdateCommand = new OleDbCommand("UPDATE LearnerSubject SET(@LearnerID, @SubjectName, @Result) WHERE LearnerID = @LearnerID AND SubjectName = @SubjectName", cnMain);
            daSubject.UpdateCommand = new OleDbCommand("UPDATE Subject SET(@SubjectName, @SubjectDescription) WHERE SubjectName = @SubjectName", cnMain);
            Build_UPDATE_Parameters();
        }
        
        internal override void Create_DELETE_Command()
        {
            daLearnerSubject.DeleteCommand = new OleDbCommand("DELETE FROM LearnerSubject WHERE LearnerID = @LearnerID AND SubjectName = @SubjectName", cnMain);
            daLearnerSubject.DeleteCommand = new OleDbCommand("DELETE FROM Subject WHERE SubjectName = @SubjectName", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new  SqlParameter("@LearnerID", SqlDbType.Int,4, "LearnerID");
            daLearnerSubject.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@SubjectName", SqlDbType.NVarChar, 255, "SubjectName");
            daLearnerSubject.InsertCommand.Parameters.Add(param);
            daSubject.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Result", SqlDbType.Float, 4, "Result");
            daLearnerSubject.InsertCommand.Parameters.Add(param);
            
            param = new SqlParameter("@SubjectDescription", SqlDbType.NVarChar, 255, "SubjectDescription");
            daSubject.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new  SqlParameter("@LearnerID", SqlDbType.Int,4, "LearnerID");
            daLearnerSubject.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@SubjectName", SqlDbType.NVarChar, 255, "SubjectName");
            daLearnerSubject.UpdateCommand.Parameters.Add(param);
            daSubject.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@Result", SqlDbType.Float, 4, "Result");
            daLearnerSubject.UpdateCommand.Parameters.Add(param);
            
            param = new SqlParameter("@SubjectDescription", SqlDbType.NVarChar, 255, "SubjectDescription");
            daSubject.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new  SqlParameter("@LearnerID", SqlDbType.Int,4, "LearnerID");
            daLearnerSubject.DeleteCommand.Parameters.Add(param);    
            
            param = new SqlParameter("@SubjectName", SqlDbType.NVarChar, 255, "SubjectName");
            daLearnerSubject.DeleteCommand.Parameters.Add(param);
            daSubject.DeleteCommand.Parameters.Add(param);
        }

        #endregion


    }
}
