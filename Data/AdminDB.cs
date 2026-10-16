using FuturePathGroup10.Administration;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Principal;

namespace FuturePathGroup10.Data
{

	public class AdminDB : DB
	{

		#region Attributes

		private const string ADMIN_TABLE = "Administrator";
		private const string SQL_SELECT_ADMIN = "SELECT * FROM Administrator0;"

		public Collection<Administrator> admins;

        #endregion
        public AdminDB()
		{
			admins = new Collection<Administrator>();
			FillDataSet(SQL_SELECT_ADMIN, ADMIN_TABLE);

		}

		private void LoadAccounts()
		{
			foreach(DataRow row in dsMain.Tables[ADMIN_TABLE].Rows)
			{
				int adminID = row<int>("AdminID");
				string username = row["Username"];
				string fullName = row["FullName"];

				Administrator admin = new Administrator(adminID, fullName, username);
				admins.Add(admin);
			}
		}

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
                if (Convert.ToInt32(dsMain.Tables[ADMIN_TABLE].Rows[i]["AdminID"]) == admin.AdminID)
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

                    accounts.Add(admin);
                    break;

                case DBOperation.Edit:
                    row = findDataRow(admin);
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
                    row = findDataRow(admin);
                    if (row != null)
                    {
                        row.Delete();
                        accounts.Remove(admin);
                    }
                    else
                    {
                        throw new Exception("admin not found in Tables");
                    }
                    break;
            }

        }
    }

}
