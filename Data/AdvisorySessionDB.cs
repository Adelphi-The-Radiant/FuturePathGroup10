using System;
using System.Collections.ObjectModel;
using System.Data;
using FuturePathGroup10.Advisory;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

	public class AdvisorySessionDB : DB
	{
        #region Attributes
        private const string TABLE_SESSION = "AdvisorySession";
		private const string SQL_SELECT_SESSION = "SELECT * FROM AdvisorySession";

		public Collection<AdvisorySession> sessions { get; private set; }
		private LearnerDB learnerDB;
		private CareerAdvisorDB advisorDB;
        #endregion
        public AdvisorySessionDB(LearnerDB learnerDB, CareerAdvisorDB advisorDB)
		{
			this.learnerDB = learnerDB;
			this.advisorDB = advisorDB;
			sessions = new Collection<AdvisorySession>();
			FillDataSet(SQL_SELECT_SESSION, TABLE_SESSION);
			LoadSessions();
		}

        #region getters

        public List<AdvisorySession> getAllNotes(int learnerID)
        {
            List<AdvisorySession> output = new List<AdvisorySession>();

            foreach(AdvisorySession session in sessions)
            {
                if (session.learner.learnerID == learnerID)
                {
                    output.Add(session);
                }
            }
            return output;
        }

        #endregion

        private void LoadSessions()
		{
			foreach(DataRow row in dsMain.Tables[TABLE_SESSION].Rows)
			{
				int sessionID = Convert.ToInt32(row["SessionID"]);
				int learnerID = Convert.ToInt32(row["LearnerID"]);
				int advisorID = Convert.ToInt32(row["AdvisorID"]);
				DateTime date = row.Field<DateTime>("SessionDate");
				string notes = row["Notes"].ToString();

				Learner.Learner learner = learnerDB.getLearner(learnerID);
				CareerAdvisor advisor = advisorDB.getAdvisor(advisorID);

				AdvisorySession session = new AdvisorySession(sessionID, learner, advisor, date, notes);
				sessions.Add(session);
			}

		}

        #region DataSet update
		private void FillDataRow(DataRow row,AdvisorySession session)
		{
			row["SessionID"] = session.sessionID;
			row["LearnerID"] = session.learner.learnerID;
			row["AdvisorID"] = session.advisor.advisorID;
			row["SessionDate"] = session.date;
			row["Notes"] = session.notes;
		}

		private DataRow FindDataRow(AdvisorySession session)
		{
            for(int i = 0; i < dsMain.Tables[TABLE_SESSION].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsMain.Tables[TABLE_SESSION].Rows[i]["SessionID"]) == session.sessionID)
                {
                    return dsMain.Tables[TABLE_SESSION].Rows[i];
                }
            }

            return null;
        }

        public void DataSetChange(AdvisorySession session, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsMain.Tables[TABLE_SESSION].NewRow();
                    FillDataRow(row, session);
                    dsMain.Tables[TABLE_SESSION].Rows.Add(row);

                    sessions.Add(session);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(session);
                    if (row != null)
                    {
                        FillDataRow(row, session);
                    }
                    else
                    {
                        throw new Exception("session not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(session);
                    if (row != null)
                    {
                        row.Delete();
                        sessions.Remove(session);
                    }
                    else
                    {
                        throw new Exception("session not found in Tables");
                    }
                    break;
            }
        }
        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            return UpdateDataSource(SQL_SELECT_SESSION, TABLE_SESSION);
        }

        internal override void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO AdvisorySession (@SessionID,@LearnerID,@AdvisorID,@SessionDate,@Notes) VALUES (SessionID,LearnerID,AdvisorID,SessionDate,Notes)",cnMain);
            Build_INSERT_Parameters();
        }
        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE AdvisorySession SET (@SessionID,@LearnerID,@AdvisorID,@SessionDate,@Notes) WHERE SessionID = @SessionID",cnMain);
            Build_UPDATE_Parameters();
        }
        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM AdvisorySession WHERE SessionID = @SessionID",cnMain);
            Build_DELETE_Parameters();
        }
        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@SessionID", SqlDbType.Int, 4, "SessionID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@AdvisorID", SqlDbType.Int, 4, "AdvisorID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@SessionDate", SqlDbType.DateTime, 255, "SessionDate");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Notes", SqlDbType.NChar, 255, "Notes");
            daMain.InsertCommand.Parameters.Add(param);
        }
        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@SessionID", SqlDbType.Int, 4, "SessionID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@LearnerID", SqlDbType.Int, 4, "LearnerID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@AdvisorID", SqlDbType.Int, 4, "AdvisorID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@SessionDate", SqlDbType.DateTime, 255, "SessionDate");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Notes", SqlDbType.NChar, 255, "Notes");
            daMain.UpdateCommand.Parameters.Add(param);

        }
        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@SessionID", SqlDbType.Int, 4, "SessionID");
            daMain.DeleteCommand.Parameters.Add(param);
        }
        #endregion
    }
}
