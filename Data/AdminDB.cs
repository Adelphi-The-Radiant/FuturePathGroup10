using FuturePathGroup10.Administration;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Principal;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

	public class AdminDB : DB
	{

		#region Attributes

		private const string ADMIN_TABLE = "Administrator";
        private const string SQL_SELECT_ADMIN = "SELECT * FROM Administrator;";


        public Collection<Administrator> admins { get; private set; }

        #endregion
        public AdminDB()
		{
			admins = new Collection<Administrator>();
			FillDataSet(SQL_SELECT_ADMIN, ADMIN_TABLE);
            LoadAdmins();
		}

		private void LoadAdmins()
		{
			foreach(DataRow row in dsMain.Tables[ADMIN_TABLE].Rows)
			{

				int adminID = Convert.ToInt32(row["AdminID"]);
				string username = row["Username"].ToString();
				string fullName = row["FullName"].ToString();

				Administrator admin = new Administrator(adminID, fullName, username);
				admins.Add(admin);
			}
		}

        #region Database Updates

        private void FillDataRow(DataRow row, Administrator admin)
		{
			row["AdminID"] = admin.adminID;
			row["Username"] = admin.username;
			row["FullName"] = admin.fullName;
		}

        private DataRow FindDataRow(Administrator admin)
        {
            for (int i = 0; i < dsMain.Tables[ADMIN_TABLE].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsMain.Tables[ADMIN_TABLE].Rows[i]["AdminID"]) == admin.adminID)
                {
                    return dsMain.Tables[ADMIN_TABLE].Rows[i];
                }
            }

            return null;
        }

        public void DataSetChange(Administrator admin, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsMain.Tables[ADMIN_TABLE].NewRow();
                    FillDataRow(row, admin);
                    dsMain.Tables[ADMIN_TABLE].Rows.Add(row);

                    admins.Add(admin);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(admin);
                    if (row != null)
                    {
                        FillDataRow(row, admin);
                    }
                    else
                    {
                        throw new Exception("admin not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(admin);
                    if (row != null)
                    {
                        row.Delete();
                        admins.Remove(admin);
                    }
                    else
                    {
                        throw new Exception("admin not found in Tables");
                    }
                    break;
            }
        }

        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            return UpdateDataSource(SQL_SELECT_ADMIN, ADMIN_TABLE);
        }

        internal override void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO Administrator (@AdminID, @Username, @FullName) VALUES (AdminID, Username, FullName)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE Administrator SET (@AdminID, @Username, @FullName) WHERE AdminID = @AdminID", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM Administrator WHERE AdminID = @AdminID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@AdminID", SqlDbType.Int, 32, "AdminID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Username", SqlDbType.NChar, 255, "Username");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@FullName", SqlDbType.NChar, 255, "FullName");
            daMain.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@AdminID", SqlDbType.Int, 32, "AdminID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Username", SqlDbType.NChar, 255, "Username");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@FullName", SqlDbType.NChar, 255, "FullName");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@AdminID", SqlDbType.Int, 32, "AdminID");
            daMain.DeleteCommand.Parameters.Add(param);
        }

        #endregion


    }

}
