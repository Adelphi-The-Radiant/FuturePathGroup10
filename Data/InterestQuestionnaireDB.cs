using FuturePathGroup10.Interest;
using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

    public class InterestQuestionnaireDB : DB
	{
        #region Attributes
        private const string SQL_SELECT_JOINED = "SELECT * FROM " +
        "(SELECT * FROM InterestQuestionnaire LEFT JOIN InterestQuestion " +
        "ON InterestQuestionnaire.QuestionID = InterestQuestion.QuestionID " +
        "ORDER BY InterestQuestionnaire.ResponseID)" +
        " AS JoinedTable";
        private const string TABLE_JOINED = "JoinedTable";

        private const string TABLE_QUESTIONNAIRE = "InterestQuestionnaire";
        private const string TABLE_QUESTION = "InterestQuestion";

        private const string SQL_SELECT_QUESTIONNAIRE = "SELECT * FROM InterestQuestionnaire";
        private const string SQL_SELECT_QUESTION = "SELECT * FROM InterestQuestion";

        public Collection<InterestQuestionnaire> questionnaires { get; private set; }
        public Collection<InterestQuestion> questions { get; private set; }

        public LearnerInterestResponseDB LIRDB;

        public DataSet dsQuestionnaire;
        public DataSet dsQuestion;

        public OleDbDataAdapter daQuestionnaire;
        public OleDbDataAdapter daQuestion;

        #endregion

        #region Constructors
        public InterestQuestionnaireDB(LearnerInterestResponseDB LIRDB)
		{
            this.LIRDB = LIRDB;
            questionnaires = new Collection<InterestQuestionnaire>();
            questions = new Collection<InterestQuestion>();

            FillDataSet(SQL_SELECT_QUESTIONNAIRE, TABLE_QUESTIONNAIRE);
            dsQuestionnaire = dsMain;
            daQuestionnaire = daMain;

            FillDataSet(SQL_SELECT_QUESTION, TABLE_QUESTION);
            dsQuestion = dsMain;
            daQuestion = daMain;

            FillDataSet(SQL_SELECT_JOINED, TABLE_JOINED);

            LoadQuestionnaires();
		}

        #endregion

        private void LoadQuestionnaires()
        {
            int questionID = 0;
            int candidateResponseID = 0;
            int responseID = -1;

            int interestCategoryID = 0;
            string response = "";
            string question = "";

            InterestQuestion interestQuestion = null;
            InterestQuestionnaire questionnaire = null;
            LearnerInterestResponse LIR = null;

            foreach(DataRow row in dsMain.Tables[TABLE_JOINED].Rows)
            {
                candidateResponseID = Convert.ToInt32(row["QuestionID"]);

                if (responseID != candidateResponseID)
                {
                    questionID = candidateResponseID;
                    response = row["Response"].ToString();
                    responseID = Convert.ToInt32(row["ResponseID"]);
                    LIR = LIRDB.getLIR(responseID);

                    questionnaire = new InterestQuestionnaire(LIR,response);
                    LIR.setQuestionnaire(questionnaire);

                    questionnaires.Add(questionnaire);
                }

                interestCategoryID = Convert.ToInt32(row["InterestCategoryID"]);
                question = row["Question"].ToString();
                interestQuestion = new InterestQuestion(questionID, interestCategoryID, question);
                questionnaire.addQuestion(interestQuestion);

                if (!questions.Contains(interestQuestion))
                {
                    questions.Add(interestQuestion);
                }
                

            }
        }

        #region Update Database
        private void FillDataRow(DataRow row,InterestQuestionnaire questionnaire, InterestQuestion question)
        {
            row["QuestionID"] = question.questionID;
            row["ResponseID"] = questionnaire.LIR.responseID;
            row["Response"] = questionnaire.response;
        }

        private void FillDataRow(DataRow row, InterestQuestion question) {
            row["QuestionID"] = question.questionID;
            row["InterestCategoryID"] = question.interestCategoryID;
            row["Question"] = question.question;

        }

        private DataRow FindDataRow(InterestQuestionnaire questionnaire, InterestQuestion question)
        {
            for (int i = 0; i < dsQuestionnaire.Tables[TABLE_QUESTIONNAIRE].Rows.Count; i++)
            {
                DataRow row = dsQuestionnaire.Tables[TABLE_QUESTIONNAIRE].Rows[i];

                if (Convert.ToInt32(row["QuestionID"]) == question.questionID && Convert.ToInt32(row["ResponseID"]) == questionnaire.LIR.responseID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDataRow(InterestQuestion question)
        {
            for(int i = 0; i < dsQuestion.Tables[TABLE_QUESTION].Rows.Count; i++)
            {
                DataRow row = dsQuestion.Tables[TABLE_QUESTION].Rows[i];

                if (Convert.ToInt32(row["QuestionID"]) == question.questionID) { 
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(InterestQuestionnaire questionnaire, InterestQuestion question, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsQuestionnaire.Tables[TABLE_QUESTIONNAIRE].NewRow();
                    FillDataRow(row, questionnaire,question);
                    dsQuestionnaire.Tables[TABLE_QUESTIONNAIRE].Rows.Add(row);

                    questionnaires.Add(questionnaire);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(questionnaire,question);
                    if (row != null)
                    {
                        FillDataRow(row, questionnaire, question);
                    }
                    else
                    {
                        throw new Exception("LearnerInterestResponse not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(questionnaire, question);
                    if (row != null)
                    {
                        row.Delete();
                        questionnaires.Remove(questionnaire);
                    }
                    else
                    {
                        throw new Exception("LearnerInterestResponse not found in Tables");
                    }
                    break;
            }
        }

        public void DataSetChange(InterestQuestion question, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsQuestion.Tables[TABLE_QUESTION].NewRow();
                    FillDataRow(row,question);
                    dsQuestionnaire.Tables[TABLE_QUESTION].Rows.Add(row);

                    questions.Add(question);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(question);
                    if (row != null)
                    {
                        FillDataRow(row, question);
                    }
                    else
                    {
                        throw new Exception("LearnerInterestResponse not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(question);
                    if (row != null)
                    {
                        row.Delete();
                        questions.Remove(question);
                    }
                    else
                    {
                        throw new Exception("LearnerInterestResponse not found in Tables");
                    }
                    break;
            }
        }

        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            dsMain = dsQuestionnaire;
            bool check1 = UpdateDataSource(SQL_SELECT_QUESTIONNAIRE, TABLE_QUESTIONNAIRE, daQuestionnaire);
            dsQuestionnaire = dsMain;
            dsMain = dsQuestion;
            bool check2 = UpdateDataSource(SQL_SELECT_QUESTION, TABLE_QUESTION, daQuestion);
            dsQuestion = dsMain;
            return check1 && check2;
        }

        internal override void Create_INSERT_Command()
        {
            daQuestionnaire.InsertCommand = new OleDbCommand("INSERT INTO InterestQuestionnaire (@ResponseID,@QuestionID,@Response) VALUES (ResponseID,QuestionID,Response)", cnMain);
            daQuestion.InsertCommand = new OleDbCommand("INSERT INTO InterestQuestion (@QuestionID, @InterestCategoryID, @Question) VALUES (QuestionID, InterestCategoryID, Question)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daQuestionnaire.UpdateCommand = new OleDbCommand("UPDATE InterestQuestionnaire SET (@ResponseID,@QuestionID,@Response) WHERE ResponseID = @ResponseID AND QuestionID = @QuestionID", cnMain);
            daQuestion.UpdateCommand = new OleDbCommand("Update InterestQuestion SET (@QuestionID, @InterestCategoryID, @Question) WHERE  QuestionID = @QuestionID", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daQuestionnaire.DeleteCommand = new OleDbCommand("DELETE FROM InterestQuestionnaire WHERE ResponseID = @ResponseID AND QuestionID = @QuestionID");
            daQuestion.DeleteCommand = new OleDbCommand("DELETE FROM InterestQuestion WHERE QuestionID = @QuestionID");
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@ResponseID", SqlDbType.Int, 4, "ResponseID");
            daQuestionnaire.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID");
            daQuestionnaire.InsertCommand.Parameters.Add(param);
            daQuestion.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@InterestCategoryID", SqlDbType.Int, 4, "InterestCategoryID");
            daQuestion.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Response", SqlDbType.NVarChar, 255, "Response");
            daQuestionnaire.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Question", SqlDbType.NVarChar, 255, "Question");
            daQuestion.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@ResponseID", SqlDbType.Int, 4, "ResponseID");
            daQuestionnaire.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID");
            daQuestionnaire.UpdateCommand.Parameters.Add(param);
            daQuestion.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@InterestCategoryID", SqlDbType.Int, 4, "InterestCategoryID");
            daQuestion.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Response", SqlDbType.NVarChar, 255, "Response");
            daQuestionnaire.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Question", SqlDbType.NVarChar, 255, "Question");
            daQuestion.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@ResponseID", SqlDbType.Int, 4, "ResponseID");
            daQuestionnaire.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID");
            daQuestionnaire.DeleteCommand.Parameters.Add(param);
            daQuestion.DeleteCommand.Parameters.Add(param);
        }

        #endregion
    }
}
