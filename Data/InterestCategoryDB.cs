using FuturePathGroup10.Advisory;
using FuturePathGroup10.Interest;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

	public class InterestCategoryDB : DB
	{
		#region Attributes

		private const string SQL_SELECT_JOINED = "SELECT * FROM " +
			"(SELECT * FROM InterestCategory LEFT JOIN QuestionCategoryMapping ON InterestCategory.InterestCategoryID = QuestionCategoryMapping.InterestCategoryID " +
			"ORDER BY InterestCategory.InterestCategoryID) " +
			"AS JoinedTable";

		private const string TABLE_JOINED = "JoinedTable";
		private const string TABLE_QCM = "QuestionCategoryMapping";
		private const string TABLE_CATEGORY = "InterestCategory";
		private const string SQL_SELECT_QCM = "SELECT * FROM QuestionCategoryMapping";
		private const string SQL_SELECT_CATEGORY = "SELECT * FROM InterestCategory";

		private DataSet dsQCM;
		private DataSet dsCategory;

		private OleDbDataAdapter daQCM;
		private OleDbDataAdapter daCategory;
		private InterestQuestionnaireDB IQDB;

		public Collection<InterestCategory> categories { get; private set; }

        #endregion
        public InterestCategoryDB(InterestQuestionnaireDB IQDB)
		{
			this.IQDB = IQDB;
			categories = new Collection<InterestCategory>();

			FillDataSet(SQL_SELECT_QCM, TABLE_QCM);
			dsQCM = dsMain;
			daQCM = daMain;

			FillDataSet(SQL_SELECT_CATEGORY, TABLE_CATEGORY);
			dsCategory = dsMain;
			daQCM = daMain;

			FillDataSet(SQL_SELECT_JOINED, TABLE_JOINED);

			LoadCategories();
		}

		private void LoadCategories()
		{
			int interestCategoryID = -1;
			InterestCategory category = null;

            foreach (DataRow row in dsMain.Tables[TABLE_JOINED].Rows)
			{
				int candidateID = Convert.ToInt32(row["InterestCategoryID"]);

				if (interestCategoryID != candidateID)
				{
					interestCategoryID = candidateID;
					string title = row["Title"].ToString();
					string description = row["Description"].ToString();

					category = new InterestCategory(interestCategoryID,title,description);
				}
				int questionID = Convert.ToInt32(row["QuestionID"]);

				InterestQuestion question = IQDB.getQuestion(questionID);
                question.addCategory(category);

			}
		}

        #region Update DataSet

		private void FillDataRow(DataRow row, InterestCategory category)
		{
			row["InterestCategoryID"] = category.interestCategoryID;
			row["Title"] = category.title;
			row["Description"] = category.description;
		}

        private void FillDataRow(DataRow row, InterestCategory category,InterestQuestion question)
        {
            row["InterestCategoryID"] = category.interestCategoryID;
            row["QuestionID"] = question.questionID;
        }

        private DataRow FindDataRow(InterestCategory category)
		{
			foreach(DataRow row in dsCategory.Tables[TABLE_CATEGORY].Rows)
			{
				if (Convert.ToInt32(row["InterestCategoryID"]) == category.interestCategoryID)
				{
					return row;
				}
			}
			return null;
		}

        private DataRow FindDataRow(InterestCategory category, InterestQuestion question)
        {
            foreach (DataRow row in dsQCM.Tables[TABLE_QCM].Rows)
            {
                if (Convert.ToInt32(row["InterestCategoryID"]) == category.interestCategoryID && Convert.ToInt32(row["QuestionID"]) == question.questionID)
                {
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(InterestCategory category, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsCategory.Tables[TABLE_CATEGORY].NewRow();
                    FillDataRow(row, category);
                    dsCategory.Tables[TABLE_CATEGORY].Rows.Add(row);

                    categories.Add(category);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(category);
                    if (row != null)
                    {
                        FillDataRow(row, category);
                    }
                    else
                    {
                        throw new Exception("category not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(category);
                    if (row != null)
                    {
                        row.Delete();
                        categories.Remove(category);
                    }
                    else
                    {
                        throw new Exception("category not found in Tables");
                    }
                    break;
            }
        }

        public void DataSetChange(InterestCategory category, InterestQuestion question, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsQCM.Tables[TABLE_QCM].NewRow();
                    FillDataRow(row, category);
                    dsQCM.Tables[TABLE_QCM].Rows.Add(row);

                    categories.Add(category,question);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(category,question);
                    if (row != null)
                    {
                        FillDataRow(row, category,question);
                    }
                    else
                    {
                        throw new Exception("category or question not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(category,question);
                    if (row != null)
                    {
                        row.Delete();
                        categories.Remove(category);
                    }
                    else
                    {
                        throw new Exception("category or question not found in Tables");
                    }
                    break;
            }
        }
        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            dsMain = dsCategory;
            bool check1 = UpdateDataSource(SQL_SELECT_CATEGORY, TABLE_CATEGORY, daCategory);
            dsCategory = dsMain;
            dsMain = dsQCM;
            bool check2 = UpdateDataSource(SQL_SELECT_QCM, TABLE_QCM, daQCM);
            dsQCM = dsMain;
        }

        internal override void Create_INSERT_Command()
        {
            daCategory.InsertCommand = new OleDbCommand("INSERT INTO InterestCategory (@InterestCategoryID,@Title,@Description) VALUES (InterestCategoryID,Title,Description)", cnMain);
            daQCM.InsertCommand = new OleDbCommand("INSERT INTO QuestionCategoryMapping (@QuestionID,@InterestCategoryID) VALUES (QuestionID,InterestCategoryID)",cnMain);
            Build_INSERT_Parameters();     
        }

        internal override void Create_UPDATE_Command()
        {
            daCategory.UpdateCommand = new OleDbCommand("UPDATE InterestCategory SET (@InterestCategoryID,@Title,@Description) WHERE InterestCategoryID = @InterestCategoryID", cnMain);
            daQCM.UpdateCommand = new OleDbCommand("UPDATE QuestionCategoryMapping SET (@QuestionID,@InterestCategoryID) " +
                "WHERE InterestCategoryID = @InterestCategoryID AND QuestionID = @QuestionID", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daCategory.DeleteCommand = new OleDbCommand("DELETE FROM InterestCategory WHERE InterestCategoryID = @InterestCategoryID", cnMain);
            daQCM.DeleteCommand = new OleDbCommand("DELETE FROM QuestionCategoryMapping WHERE InterestCategoryID = @InterestCategoryID AND QuestionID = @QuestionID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@InterestCategoryID", SqlDbType.Int, 4, "InterestCategoryID");
            daCategory.InsertCommand.Parameters.Add(param);
            daQCM.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID");
            daQCM.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daCategory.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, 255, "Description");
            daCategory.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@InterestCategoryID", SqlDbType.Int, 4, "InterestCategoryID");
            daCategory.UpdateCommand.Parameters.Add(param);
            daQCM.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID");
            daQCM.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Title", SqlDbType.NVarChar, 255, "Title");
            daCategory.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NVarChar, 255, "Description");
            daCategory.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@InterestCategoryID", SqlDbType.Int, 4, "InterestCategoryID");
            daCategory.DeleteCommand.Parameters.Add(param);
            daQCM.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@QuestionID", SqlDbType.Int, 4, "QuestionID");
            daQCM.DeleteCommand.Parameters.Add(param);
        }

        #endregion
    }
}
