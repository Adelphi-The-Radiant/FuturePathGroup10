using FuturePathGroup10.Interest;
using FuturePathGroup10.Learner;
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
        private const string SQL_SELECT_JOINED = "SELECT InterestQuestionnaire.QuestionID AS QuestionID, InterestQuestionnaire.ResponseID AS ResponseID, InterestQuestionnaire.Response AS Response, InterestQuestion.InterestCategoryID AS InterestCategoryID, InterestQuestion.Question AS Question " +
        "FROM InterestQuestionnaire LEFT JOIN InterestQuestion ON InterestQuestionnaire.QuestionID = InterestQuestion.QuestionID " +
        "ORDER BY InterestQuestionnaire.ResponseID";
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

            LoadQuestions();
            LoadQuestionnaires();
        }

        #endregion

        public InterestQuestion getQuestion(int questionID)
        {
            foreach (InterestQuestion question in questions)
            {
                if (question.questionID == questionID)
                {
                    return question;
                }
            }
            return null;
        }

        private void LoadQuestions()
        {
            foreach (DataRow row in dsQuestion.Tables[TABLE_QUESTION].Rows)
            {
                int questionID = Convert.ToInt32(row["QuestionID"]);
                int interestCategoryID = row["InterestCategoryID"] is DBNull ? 0 : Convert.ToInt32(row["InterestCategoryID"]);
                string question = row["Question"].ToString();

                questions.Add(new InterestQuestion(questionID, interestCategoryID, question));
            }
        }

        private void LoadQuestionnaires()
        {
            int responseID = -1;

            InterestQuestionnaire questionnaire = null;
            LearnerInterestResponse LIR = null;

            foreach (DataRow row in dsMain.Tables[TABLE_JOINED].Rows)
            {
                int rowResponseID = Convert.ToInt32(row["ResponseID"]);

                if (rowResponseID != responseID)
                {
                    responseID = rowResponseID;
                    LIR = LIRDB.getLIR(responseID);

                    if (LIR == null)
                    {
                        questionnaire = null;
                        continue;
                    }

                    questionnaire = new InterestQuestionnaire(LIR, row["Response"].ToString());
                    LIR.setQuestionnaire(questionnaire);

                    questionnaires.Add(questionnaire);
                }

                if (questionnaire == null) continue;

                InterestQuestion interestQuestion = getQuestion(Convert.ToInt32(row["QuestionID"]));
                if (interestQuestion != null)
                {
                    questionnaire.addQuestion(interestQuestion);
                }
            }
        }

        #region Update Database
        private void FillDataRow(DataRow row, InterestQuestionnaire questionnaire, InterestQuestion question)
        {
            row["QuestionID"] = question.questionID;
            row["ResponseID"] = questionnaire.LIR.responseID;
            row["Response"] = questionnaire.response;
        }

        private void FillDataRow(DataRow row, InterestQuestion question)
        {
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
            for (int i = 0; i < dsQuestion.Tables[TABLE_QUESTION].Rows.Count; i++)
            {
                DataRow row = dsQuestion.Tables[TABLE_QUESTION].Rows[i];

                if (Convert.ToInt32(row["QuestionID"]) == question.questionID)
                {
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
                    FillDataRow(row, questionnaire, question);
                    dsQuestionnaire.Tables[TABLE_QUESTIONNAIRE].Rows.Add(row);

                    questionnaires.Add(questionnaire);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(questionnaire, question);
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
                    FillDataRow(row, question);
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

        private static OleDbParameter IntParam(string name, string column)
        {
            return new OleDbParameter(name, OleDbType.Integer, 4, column);
        }

        private static OleDbParameter TextParam(string name, string column)
        {
            return new OleDbParameter(name, OleDbType.VarWChar, 255, column);
        }

        internal override void Create_INSERT_Command()
        {
            daQuestionnaire.InsertCommand = new OleDbCommand("INSERT INTO InterestQuestionnaire (ResponseID, QuestionID, Response) VALUES (?, ?, ?)", cnMain);
            daQuestion.InsertCommand = new OleDbCommand("INSERT INTO InterestQuestion (QuestionID, InterestCategoryID, Question) VALUES (?, ?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daQuestionnaire.UpdateCommand = new OleDbCommand("UPDATE InterestQuestionnaire SET Response = ? WHERE ResponseID = ? AND QuestionID = ?", cnMain);
            daQuestion.UpdateCommand = new OleDbCommand("UPDATE InterestQuestion SET InterestCategoryID = ?, Question = ? WHERE QuestionID = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daQuestionnaire.DeleteCommand = new OleDbCommand("DELETE FROM InterestQuestionnaire WHERE ResponseID = ? AND QuestionID = ?", cnMain);
            daQuestion.DeleteCommand = new OleDbCommand("DELETE FROM InterestQuestion WHERE QuestionID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daQuestionnaire.InsertCommand.Parameters.Add(IntParam("@ResponseID", "ResponseID"));
            daQuestionnaire.InsertCommand.Parameters.Add(IntParam("@QuestionID", "QuestionID"));
            daQuestionnaire.InsertCommand.Parameters.Add(TextParam("@Response", "Response"));

            daQuestion.InsertCommand.Parameters.Add(IntParam("@QuestionID", "QuestionID"));
            daQuestion.InsertCommand.Parameters.Add(IntParam("@InterestCategoryID", "InterestCategoryID"));
            daQuestion.InsertCommand.Parameters.Add(TextParam("@Question", "Question"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daQuestionnaire.UpdateCommand.Parameters.Add(TextParam("@Response", "Response"));
            daQuestionnaire.UpdateCommand.Parameters.Add(IntParam("@ResponseID", "ResponseID"));
            daQuestionnaire.UpdateCommand.Parameters.Add(IntParam("@QuestionID", "QuestionID"));

            daQuestion.UpdateCommand.Parameters.Add(IntParam("@InterestCategoryID", "InterestCategoryID"));
            daQuestion.UpdateCommand.Parameters.Add(TextParam("@Question", "Question"));
            daQuestion.UpdateCommand.Parameters.Add(IntParam("@QuestionID", "QuestionID"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daQuestionnaire.DeleteCommand.Parameters.Add(IntParam("@ResponseID", "ResponseID"));
            daQuestionnaire.DeleteCommand.Parameters.Add(IntParam("@QuestionID", "QuestionID"));

            daQuestion.DeleteCommand.Parameters.Add(IntParam("@QuestionID", "QuestionID"));
        }

        #endregion
    }
}
