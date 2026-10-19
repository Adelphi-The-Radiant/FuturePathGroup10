using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Learner;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.DirectoryServices;

namespace FuturePathGroup10.Data
{
    //Used to get access to learnerSubject instances from the DB accessed via the learnerSubjects collection.
    //This class also fills up the learner Collection from LearnerDB which is required.
    //Obviously also allows editing of the DB through DataSetChange and UpdateDataSource TOGETHER
    public class SubjectDB : DB
    {
        #region Attributes
        private const string SQL_SELECT_SUBJECT_JOIN = "SELECT Subject.SubjectName AS SubjectName, Subject.SubjectDescription AS SubjectDescription, LearnerSubject.LearnerID AS LearnerID, LearnerSubject.Result AS Result FROM Subject LEFT JOIN LearnerSubject ON Subject.SubjectName = LearnerSubject.SubjectName ORDER BY Subject.SubjectName";
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
            dsSubject = dsMain;
            daSubject = daMain;

            //dsMain and daMain remain for the joined table. I will probably have to change this later.
            FillDataSet(SQL_SELECT_SUBJECT_JOIN, JOINED_TABLE);

            LoadLearnerSubjects();
        }
        #endregion
        #region getters
        public Subject getSubject(string subjectName)
        {
            foreach (Subject subject in subjects)
            {
                if (subject.subjectName.Equals(subjectName))
                {
                    return subject;
                }
            }
            return null;
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
            float result;

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


                try
                {
                    result = Convert.ToSingle(row["Result"]);
                }
                catch (Exception)
                {
                    result = 0;
                }

                try
                {
                    learner = learnerDB.getLearner(Convert.ToInt32(row["LearnerID"]));
                }
                catch (Exception)
                {
                    learner = null;
                }

                learnerSubject = new LearnerSubject(learner, subject, result);

                learnerSubjects.Add(learnerSubject);
                if (learner != null)
                {
                    learner.addLearnerSubject(learnerSubject);
                }
            }

        }
        #endregion

        #region Dataset Updates
        private void FillLearnerSubjectRow(DataRow row, LearnerSubject learnerSubject)
        {
            row["LearnerID"] = learnerSubject.learner.learnerID;
            row["SubjectName"] = learnerSubject.subject.subjectName;
            row["Result"] = learnerSubject.result;
        }

        private void FillSubjectRow(DataRow row, LearnerSubject learnerSubject)
        {
            row["SubjectName"] = learnerSubject.subject.subjectName;
            row["SubjectDescription"] = learnerSubject.subject.subjectDescription;
        }

        private DataRow FindLearnerSubjectRow(LearnerSubject learnerSubject)
        {
            for (int i = 0; i < dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].Rows.Count; i++)
            {
                DataRow row = dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].Rows[i];
                if (row.RowState == DataRowState.Deleted) continue;

                //Two primary keys so we check both...
                if (Convert.ToInt32(row["LearnerID"]) == learnerSubject.learner.learnerID
                    && row["SubjectName"].ToString() == learnerSubject.subject.subjectName)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindSubjectRow(LearnerSubject learnerSubject)
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
            switch (operation)
            {
                case DBOperation.Add:
                    rowLearnerSubject = FindLearnerSubjectRow(learnerSubject);
                    if (rowLearnerSubject != null)
                    {
                        FillLearnerSubjectRow(rowLearnerSubject, learnerSubject);

                        LearnerSubject existing = FindLoadedLearnerSubject(learnerSubject);
                        if (existing != null)
                        {
                            existing.result = learnerSubject.result;
                            break;
                        }
                    }
                    else
                    {
                        rowLearnerSubject = dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].NewRow();
                        FillLearnerSubjectRow(rowLearnerSubject, learnerSubject);
                        dsLearnerSubject.Tables[LEARNERSUBJECT_TABLE].Rows.Add(rowLearnerSubject);
                    }

                    learnerSubjects.Add(learnerSubject);
                    learnerSubject.learner.addLearnerSubject(learnerSubject);
                    break;
                case DBOperation.Edit:
                    rowLearnerSubject = FindLearnerSubjectRow(learnerSubject);
                    if (rowLearnerSubject != null)
                    {
                        FillLearnerSubjectRow(rowLearnerSubject, learnerSubject);
                    }
                    else
                    {
                        throw new Exception("LearnerSubject not found in table");
                    }
                    break;
                case DBOperation.Delete:
                    rowLearnerSubject = FindLearnerSubjectRow(learnerSubject);
                    if (rowLearnerSubject != null)
                    {
                        rowLearnerSubject.Delete();
                        learnerSubjects.Remove(learnerSubject);
                        learnerSubject.learner.removeLearnerSubject(learnerSubject);
                    }
                    else
                    {
                        throw new Exception("LearnerSubject not found in table");
                    }
                    break;
            }
        }

        private LearnerSubject FindLoadedLearnerSubject(LearnerSubject learnerSubject)
        {
            foreach (LearnerSubject loaded in learnerSubjects)
            {
                if (loaded.learner != null
                    && loaded.learner.learnerID == learnerSubject.learner.learnerID
                    && loaded.subject.subjectName == learnerSubject.subject.subjectName)
                {
                    return loaded;
                }
            }
            return null;
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

        private static OleDbParameter Param(string name, OleDbType type, int size, string sourceColumn)
        {
            return new OleDbParameter(name, type, size, sourceColumn);
        }

        internal override void Create_INSERT_Command()
        {
            daLearnerSubject.InsertCommand = new OleDbCommand("INSERT INTO LearnerSubject (LearnerID, SubjectName, Result) VALUES (?, ?, ?)", cnMain);
            daSubject.InsertCommand = new OleDbCommand("INSERT INTO Subject (SubjectName, SubjectDescription) VALUES (?, ?)", cnMain);
            Build_INSERT_Parameters();
            Create_UPDATE_Command();
            Create_DELETE_Command();
        }

        internal override void Create_UPDATE_Command()
        {
            daLearnerSubject.UpdateCommand = new OleDbCommand("UPDATE LearnerSubject SET Result = ? WHERE LearnerID = ? AND SubjectName = ?", cnMain);
            daSubject.UpdateCommand = new OleDbCommand("UPDATE Subject SET SubjectDescription = ? WHERE SubjectName = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daLearnerSubject.DeleteCommand = new OleDbCommand("DELETE FROM LearnerSubject WHERE LearnerID = ? AND SubjectName = ?", cnMain);
            daSubject.DeleteCommand = new OleDbCommand("DELETE FROM Subject WHERE SubjectName = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daLearnerSubject.InsertCommand.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daLearnerSubject.InsertCommand.Parameters.Add(Param("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));
            daLearnerSubject.InsertCommand.Parameters.Add(Param("@Result", OleDbType.Double, 8, "Result"));

            daSubject.InsertCommand.Parameters.Add(Param("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));
            daSubject.InsertCommand.Parameters.Add(Param("@SubjectDescription", OleDbType.VarWChar, 255, "SubjectDescription"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daLearnerSubject.UpdateCommand.Parameters.Add(Param("@Result", OleDbType.Double, 8, "Result"));
            daLearnerSubject.UpdateCommand.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daLearnerSubject.UpdateCommand.Parameters.Add(Param("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));

            daSubject.UpdateCommand.Parameters.Add(Param("@SubjectDescription", OleDbType.VarWChar, 255, "SubjectDescription"));
            daSubject.UpdateCommand.Parameters.Add(Param("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daLearnerSubject.DeleteCommand.Parameters.Add(Param("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daLearnerSubject.DeleteCommand.Parameters.Add(Param("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));

            daSubject.DeleteCommand.Parameters.Add(Param("@SubjectName", OleDbType.VarWChar, 255, "SubjectName"));
        }

        #endregion


    }
}
