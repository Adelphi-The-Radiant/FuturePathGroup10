using FuturePathGroup10;
using FuturePathGroup10.Learner;
using FuturePathGroup10.Prerequisites;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Web;
using System.Data.OleDb;
using System.Data.SqlClient;


namespace FuturePathGroup10.Data
{
	public class UserAccountDB : DB
	{
        #region Attributes
        private const USERACCOUNT_TABLE = "UserAccount";
		private const SQL_SELECT_USERACCOUNT = "SELECT * FROM UserAccount";
		public Collection<UserAccount> accounts { get; private set; }
		private LearnerDB learnerDB;
        //private AdminDB adminDB;
        //private AdvisorDB advisorDB;
        #endregion
        #region Constructor
        public UserAccountDB(LearnerDB learnerDB); //AdminDB adminDB, AdvisorDB advisorDB
		{
			accounts = new Collection<UserAccount>();
			FillDataSet(SQL_SELECT_USERACCOUNT, USERACCOUNT_TABLE);
			this.learnerDB = learnerDB;
            //this.adminDB = adminDB
            //this.advisorDB = advisorDB

            LoadAccounts();
		}
        #endregion

		private void LoadAccounts()
		{
			foreach (DataRow row in dsMain.Tables[USERACCOUNT_TABLE].Rows)
			{
                string username = row["Username"];
				string password = row["Password"];
				DateTime dateCreated = row<DateTime>["DateCreated"];
				UserAccount.Permission permissionLevel = (UserAccount.Permission) Convert.ToInt16(row["PermissionLevel"]);

				UserAccount account = new UserAccount(username, password, dateCreated, permissionLevel);
				accounts.Add(account);
				Utilities.FindAndSetAccountHolder(learnerDB.learners, account);
                //Do this for when you make an adminDB and an advisorDB
            }
		}

        #region Dataset Updates

        private void FillDataRow(DataRow row, UserAccount account)
		{
			row["Username"] = account.username;
			row["Password"] = account.password;
			row<DateTime>["DateCreated"] = account.dateCreated;
			row["PermissionLevel"] = account.permissionLevel;
		}

		private DataRow findDataRow(UserAccount account)
		{
            for (int i = 0; i < dsMain.Tables[USERACCOUNT_TABLE].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsMain.Tables[USERACCOUNT_TABLE].Rows[i]["Username"]) == account.username)
                {
                    return dsMain.Tables[USERACCOUNT_TABLE].Rows[i];
                }
            }
            return null;
        }

		public void DataSetChange(UserAccount account, DBOperation operation)
		{
            DataRow row;
			switch (operation)
			{
                case DBOperation.Add:
                    row = dsMain.Tables[USERACCOUNT_TABLE].NewRow();
                    FillLearnerRow(row,account);
                    dsMain.Tables[USERACCOUNT_TABLE].Add(learnerRow);

                    accounts.Add(account);
                    break;

                case DBOperation.Edit:
                    row = findDataRow(account);
                    if (row != null)
                    {
                        FillDataRow(row, account);
                    }
                    else
                    {
                        throw new Exception("Learner not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = findDataRow(account);
                    if (learnerRow != null && gradeRow != null && learnerIndex != -1)
                    {
                        row.Delete();
                        accounts.Remove(account);
                    }
                    else
                    {
                        throw new Exception("Learner not found in Tables");
                    }
                    break;
            }

		}
        #endregion

        #region CRUD

        public override bool UpdateAllSources()
        {
           UpdateDataSource(SQL_SELECT_USERACCOUNT, USERACCOUNT_TABLE)
        }

        internal override Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO UserAccount (@Username, @Password, @DateCreated,@PermissionLevel) VALUES (Username,Password,DateCreated,PermissionLevel)", cnMain);
            Build_INSERT_Parameters():
        }

        internal override Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE UserAccount SET (@Username, @Password, @DateCreated,@PermissionLevel) WHERE Username = @Username", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM UserAccount WHERE Username = @Username");
            Build_DELETE_Parameters();
        }

        internal override Build_INSERT_Parameters()
        {
            throw new NotImplementedException();
        }

        internal override Build_UPDATE_Parameters()
        {
            throw new NotImplementedException();
        }

        internal override Build_DELETE_Parameters()
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}
