using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace FuturePathGroup10.Data
{

	public class ReccommendationResultDB : DB
	{
        #region Attributes
        private const string TABLE_RR = "ReccommendationResult";
		private const string SQL_SELECT_RR = "SELECT * FROM ReccommendationResult";

		private LearnerDB learnerDB;
		private DegreeDB degreeDB;

		public Collection<ReccommendationResult> results { get; private set; }
        #endregion
        #region Constructor
        public ReccommendationResultDB(LearnerDB learnerDB, DegreeDB degreeDB)
		{
			this.learnerDB = learnerDB;
			this.degreeDB = degreeDB;
			results = new Collection<ReccommendationResult>();

			FillDataSet(SQL_SELECT_RR, TABLE_RR);
			LoadResults();
		}
        #endregion

        #region getters

		public List<ReccommendationResult> GetReccommendationResult(int learnerID)
		{
			List<ReccommendationResult> output = new List<ReccommendationResult>();
			foreach(ReccommendationResult result in results)
			{
				if (result.learner.learnerID == learnerID)
				{
					output.Add(result);
				}
			}
			return output;
		}

		public string getAllReccommendations(int learnerID)
		{
			string output = "";

            foreach (ReccommendationResult result in results)
            {
                if (result.learner.learnerID == learnerID)
                {
					output += result.degree.title + "\n";
                }
            }
			return output;

        }

        #endregion

        #region Loading

        private void LoadResults()
		{
			foreach(DataRow row in dsMain.Tables[TABLE_RR].Rows)
			{
				int learnerID = Convert.ToInt32(row["LearnerID"]);
				Learner.Learner learner = learnerDB.getLearner(learnerID);

				int degreeID = Convert.ToInt32(row["DegreeID"]);
				Degree degree = degreeDB.getDegree(degreeID);

				DateTime date = row.Field<DateTime>("ResultDate");

				string result = row["Results"].ToString();

				ReccommendationResult recResult = new ReccommendationResult(learner, degree, date, result);
				results.Add(recResult);
            }
		}
        #endregion

        #region Update DataSet
        private void FillDataRow(DataRow row, ReccommendationResult result)
		{
			row["LearnerID"] = result.learner.learnerID;
			row["DegreeID"] = result.degree.degreeID;
			row["ResultDate"] = result.date;
			row["Results"] = result.results;
		}

		private DataRow FindDataRow(ReccommendationResult result)
		{
			foreach (DataRow row in dsMain.Tables[TABLE_RR].Rows)
			{
				if (Convert.ToInt32(row["LearnerID"]) == result.learner.learnerID && Convert.ToInt32(row["DegreeID"]) == result.degree.degreeID)
				{
					return row;
				}
			}
			return null;
		}

		public void DataSetChange(ReccommendationResult result, DBOperation operation)
		{
			DataRow row;
			switch (operation)
			{
				case DBOperation.Add:
					row = dsMain.Tables[TABLE_RR].NewRow();
					FillDataRow(row, result);
					dsMain.Tables[TABLE_RR].Rows.Add(row);

					results.Add(result);
					break;
				case DBOperation.Edit:
					row = FindDataRow(result);
					if (row != null)
					{
						FillDataRow(row, result);
					}
					break;
				case DBOperation.Delete:
					row = FindDataRow(result);
					if (row != null)
					{
						row.Delete();
						results.Remove(result);
					}
					break;
			}
		}
        #endregion

        #region CRUD
        internal override bool UpdateAllSources()
        {
			return UpdateDataSource(SQL_SELECT_RR, TABLE_RR);
        }

        internal override void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO ReccommendationResult (LearnerID, DegreeID, ResultDate, Results) VALUES (?, ?, ?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE ReccommendationResult SET ResultDate = ?, Results = ? WHERE LearnerID = ? AND DegreeID = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM ReccommendationResult WHERE LearnerID = ? AND DegreeID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daMain.InsertCommand.Parameters.Add(CreateParam("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daMain.InsertCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
            daMain.InsertCommand.Parameters.Add(CreateParam("@ResultDate", OleDbType.Date, 8, "ResultDate"));
            daMain.InsertCommand.Parameters.Add(CreateParam("@Results", OleDbType.LongVarWChar, 0, "Results"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daMain.UpdateCommand.Parameters.Add(CreateParam("@ResultDate", OleDbType.Date, 8, "ResultDate"));
            daMain.UpdateCommand.Parameters.Add(CreateParam("@Results", OleDbType.LongVarWChar, 0, "Results"));
            daMain.UpdateCommand.Parameters.Add(CreateParam("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daMain.UpdateCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daMain.DeleteCommand.Parameters.Add(CreateParam("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daMain.DeleteCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
        }

        #endregion

    }
}
