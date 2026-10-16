using FuturePathGroup10.Interest;
using FuturePathGroup10.Learner;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Security.Principal;

namespace FuturePathGroup10.Data
{
	public class LearnerInterestResponseDB : DB
	{
        #region Attributes
        private const string TABLE_LIR = "LearnerInterestResponse";
		private const string SQL_SELECT_LIR = "SELECT * FROM LearnerInterestResponse";

		public Collection<LearnerInterestResponse> LIRs
		private LearnerDB learnerDB;
        #endregion
        public LearnerInterestResponseDB(LearnerDB learnerDB)
		{
			LIRs = new Collection<LearnerInterestResponse>();
            this.learnerDB = learnerDB;
			FillDataSet(SQL_SELECT_LIR, TABLE_LIR);
		}

		private void LoadLIR()
		{
			foreach(DataRow row in dsMain.Tables[TABLE_LIR].Rows)
			{
				int responseID = row["ResponseID"];
				int learnerID = row["LearnerID"];
				string response = row["Response"];

				Learner.Learner learner = learnerDB.getLearner(learnerID);

				LearnerInterestResponse LIRs = new LearnerInterestResponse(responseID, learner, response);
				learner.addLIR(LIR);
			}
		}

        #region Update Database

        private void FillDataRow(DataRow row, LearnerInterestResponse LIR)
		{
			row["ResponseID"] = LIR.responseID;
			row["LearnerID"] = LIR.learner.learnerID;
			row["Response"] = LIR.response;
		}

		private DataRow FindDataRow(LearnerInterestResponse LIR)
		{
            for (int i = 0; i < dsMain.Tables[TABLE_LIR].Rows.Count; i++)
            {
                if ((dsMain.Tables[TABLE_LIR].Rows[i]["ResponseID"].Equals(LIR.responseID)))
                {
                    return dsMain.Tables[TABLE_LIR].Rows[i];
                }
            }
            return null;
        }

		public void DataSetChange(LearnerInterestResponse LIR, DBOperation operation)
		{
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsMain.Tables[TABLE_LIR].NewRow();
                    FillDataRow(row, LIR);
                    dsMain.Tables[TABLE_LIR].Rows.Add(row);

                    LIRs.Add(LIR);
                    break;

                case DBOperation.Edit:
                    row = findDataRow(LIR);
                    if (row != null)
                    {
                        FillDataRow(row, LIR);
                    }
                    else
                    {
                        throw new Exception("UserAccount not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = findDataRow(LIR);
                    if (row != null)
                    {
                        row.Delete();
                        LIRs.Remove(account);
                    }
                    else
                    {
                        throw new Exception("UserAccount not found in Tables");
                    }
                    break;
            }
        }

        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            return UpdateDataSource(SQL_SELECT_LIR, TABLE_LIR);
        }

        internal override void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO LearnerInterestResponse (@ResponseID, @LearnerID, @Response) VALUES (ResponseID,LearnerID,Response)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE LearnerInterestResponse SET (@ResponseID, @LearnerID, @Response) WHERE ResponseID = @ResponseID", cnMain);
            Build_UPDATE_Parameters();
        }
        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM LearnerInterestResponse WHERE ResponseID = @ResponseID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@ResponseID", SqlDbType.Int, 4, "ResponseID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Response", SqlDbType.NChar, 255, "Response");
            daMain.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@ResponseID", SqlDbType.Int, 4, "ResponseID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Response", SqlDbType.NChar, 255, "Response");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@ResponseID", SqlDbType.Int, 4, "ResponseID");
            daMain.DeleteCommand.Parameters.Add(param);
        }

        #endregion
    }
}
