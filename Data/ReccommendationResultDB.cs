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
			daMain.InsertCommand = new OleDbCommand("INSERT INTO ReccommendationResult (@LearnerID,@DegreeID,@ResultDate,@Results) VALUES (LearnerID,DegreeID,ResultDate,Results)", cnMain);
			Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
			daMain.UpdateCommand = new OleDbCommand("UPDATE ReccommendationResult SET (@LearnerID,@DegreeID,@ResultDate,@Results) WHERE LearnerID = @LearnerID AND DegreeID = @DegreeID",cnMain);
			Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM ReccommendationResult WHERE LearnerID = @LearnerID AND DegreeID = @DegreeID", cnMain);
			Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
			SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
			daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daMain.InsertCommand.Parameters.Add(param);

			param = new SqlParameter("@ResultDate", SqlDbType.DateTime, 64, "ResultDate");
            daMain.InsertCommand.Parameters.Add(param);

			param = new SqlParameter("@Results", SqlDbType.NVarChar, 255, "Results");
            daMain.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@ResultDate", SqlDbType.DateTime, 64, "ResultDate");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Results", SqlDbType.NVarChar, 255, "Results");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
			daMain.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daMain.DeleteCommand.Parameters.Add(param);
        }


        #endregion

    }
}
