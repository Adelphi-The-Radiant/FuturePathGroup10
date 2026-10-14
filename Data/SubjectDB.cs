using System;
using System.Collections.Generic;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Learner;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

    public class SubjectDB : DB
    {
        private const string SQL_SELECT_SUBJECT_JOIN = "SELECT * FROM " +
            "(SELECT * FROM Subject LEFT JOIN LearnerSubject ON Subject.SubjectName = LearnerSubject.SubjectName ORDER BY Subject.SubjectName) " +
            "AS SubjectJoined";
        private const string JOINED_TABLE = "SubjectJoined";

        private const string SQL_SELECT_SUBJECT = "SELECT * FROM Subject";
        private const string SQL_SELECT_LEARNERSUBJECT = "SELECT * FROM LearnerSubject";

        private const string SUBJECT_TABLE = "Subject";
        private const string LEARNERSUBJECT_TABLE = "LearnerSubject";

        private Collection<LearnerSubject> learnerSubjects { get; private set; }
        private LearnerDB learnerDB;
        //Requires a learnerDB to be passed so that there is consistency between objects
        public SubjectDB(LearnerDB learnerDB) : base()
        {
            learnerSubjects = new Collection<LearnerSubject>();
            learnerDB = learnerDB;
            FillDataSet(SQL_SELECT_SUBJECT_JOIN, JOINED_TABLE);
        }
        #region Load Learner Subjects
        //This also fills up our learner object with all their subjects.
        private void LoadLearnerSubjects()
        {
            string subjectName = "Not A String";
            string candidateString;

            string subjectDecription;


            Subject subject = null;
            LearnerSubject learnerSubject;
            Learner.Learner learner;

            foreach (DataRow row in dataSet.Tables[JOINED_TABLE].Rows)
            {
                candidateString = row["SubjectName"].ToString();

                //Should ALWAYS activate on the first loop unless the subjects name is Not A String. Also on the assumption that subjectName is actually a primary key
                if (candidateString != subjectName)
                {
                    subjectName = candidateString;
                    subjectDescription = (string)row["SubjectDescription"];
                    //Ensures we dont have a bunch of duplicate subjects as this is a many to one relation. This method works because the rows will be ordered by subjectName.
                    subject = new Subject(subjectName, subjectDescription);
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
        private void fillDataRow(DataRow row, LearnerSubject learnerSubject)
        {
            row["LearnerID"] = learnerSubject.learner.learnerID;
            row["SubjectName"] = learnerSubject.subject.subjectName;
            row["SubjectDescription"] = learnerSubject.subject.subjectDescription;
            row["Result"] = learnerSubject.result;
        }

        private DataRow findDataRow(LearnerSubject learnerSubject)
        {
            for (int i = 0; i < dataSet.Tables[JOINED_TABLE].Rows.Count; i++)
            {
                DataRow row = dataSet.Tables[JOINED_TABLE].Rows[i];
                //Two primary keys so we check both...
                if (row["LearnerID"].ToString() == learnerSubject.learner.learnerID.ToString() && row["SubjectName"].ToString() == learnerSubject.subject.subjectName)
                {
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(LearnerSubject learnerSubject, DBOperation operation)
        {
            switch (operation)
            {
                case DBOperation.Add:
                    DataRow newRow = dataSet.Tables[JOINED_TABLE].NewRow();
                    fillDataRow(newRow, learnerSubject);
                    dataSet.Tables[JOINED_TABLE].Rows.Add(newRow);
                    break;
                case DBOperation.Update:
                    DataRow updateRow = findDataRow(learnerSubject);
                    if (updateRow != null)
                    {
                        fillDataRow(updateRow, learnerSubject);
                    }
                    else
                    {
                        throw new Exception("LearnerSubject not found in table")
                    }
                    break;
                case DBOperation.Delete:
                    DataRow deleteRow = findDataRow(learnerSubject);
                    if (deleteRow != null)
                    {
                        deleteRow.Delete();
                    }
                    else
                    {
                        throw new Exception("LearnerSubject not found in table")
                    }
                    break;
            }
        }

        #endregion

        #region Build Parameters, Create Commands & Update database
        //Under construction
        private void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO ")
        }

        #endregion


    }
}
