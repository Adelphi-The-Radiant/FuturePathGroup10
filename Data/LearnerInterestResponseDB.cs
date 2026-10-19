using FuturePathGroup10.Interest;
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

        public Collection<LearnerInterestResponse> LIRs;
        private LearnerDB learnerDB;
        #endregion
        public LearnerInterestResponseDB(LearnerDB learnerDB)
        {
            LIRs = new Collection<LearnerInterestResponse>();
            this.learnerDB = learnerDB;
            FillDataSet(SQL_SELECT_LIR, TABLE_LIR);
            LoadLIR();
        }

        private void LoadLIR()
        {
            foreach (DataRow row in dsMain.Tables[TABLE_LIR].Rows)
            {

                int responseID = Convert.ToInt32(row["ResponseID"]);
                int learnerID = Convert.ToInt32(row["LearnerID"]);
                string result = row["Result"].ToString();

                Learner.Learner learner = learnerDB.getLearner(learnerID);

                LearnerInterestResponse LIR = new LearnerInterestResponse(responseID, learner, result);
                LIRs.Add(LIR);
                learner?.addLIR(LIR);
            }
        }

        public LearnerInterestResponse getLIR(int responseID)
        {
            foreach (LearnerInterestResponse LIR in LIRs)
            {
                if (LIR.responseID == responseID)
                {
                    return LIR;
                }
            }
            return null;
        }

        #region Update Database

        private void FillDataRow(DataRow row, LearnerInterestResponse LIR)
        {
            row["ResponseID"] = LIR.responseID;
            row["LearnerID"] = LIR.learner.learnerID;
            row["Result"] = LIR.result;
        }

        private DataRow FindDataRow(LearnerInterestResponse LIR)
        {
            for (int i = 0; i < dsMain.Tables[TABLE_LIR].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsMain.Tables[TABLE_LIR].Rows[i]["ResponseID"]) == LIR.responseID)
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
                    row = FindDataRow(LIR);
                    if (row != null)
                    {
                        FillDataRow(row, LIR);
                    }
                    else
                    {
                        throw new Exception("LearnerInterestResponse not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(LIR);
                    if (row != null)
                    {
                        row.Delete();
                        LIRs.Remove(LIR);
                    }
                    else
                    {
                        throw new Exception("LearnerInterestResponse not found in table.");
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
            daMain.InsertCommand = new OleDbCommand("INSERT INTO LearnerInterestResponse (ResponseID, LearnerID, Result) VALUES (?, ?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE LearnerInterestResponse SET LearnerID = ?, Result = ? WHERE ResponseID = ?", cnMain);
            Build_UPDATE_Parameters();
        }
        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM LearnerInterestResponse WHERE ResponseID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daMain.InsertCommand.Parameters.Add(new OleDbParameter("@ResponseID", OleDbType.Integer, 4, "ResponseID"));
            daMain.InsertCommand.Parameters.Add(new OleDbParameter("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daMain.InsertCommand.Parameters.Add(new OleDbParameter("@Result", OleDbType.VarWChar, 255, "Result"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daMain.UpdateCommand.Parameters.Add(new OleDbParameter("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daMain.UpdateCommand.Parameters.Add(new OleDbParameter("@Result", OleDbType.VarWChar, 255, "Result"));
            daMain.UpdateCommand.Parameters.Add(new OleDbParameter("@ResponseID", OleDbType.Integer, 4, "ResponseID"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daMain.DeleteCommand.Parameters.Add(new OleDbParameter("@ResponseID", OleDbType.Integer, 4, "ResponseID"));
        }

        #endregion
    }
}
