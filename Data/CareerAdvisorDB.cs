using FuturePathGroup10.Advisory;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Principal;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

    public class CareerAdvisorDB : DB
    {

        #region Attributes

        private const string CAREER_ADVISOR_TABLE = "Administrator";

        private const string SQL_SELECT_CAREER_ADVISOR = "SELECT * FROM Administrator";


        public Collection<CareerAdvisor> advisors { get; private set; }

        #endregion
        public CareerAdvisorDB()
        {
            advisors = new Collection<CareerAdvisor>();
            FillDataSet(SQL_SELECT_CAREER_ADVISOR, CAREER_ADVISOR_TABLE);
            LoadAdvisors();

        }

        private void LoadAdvisors()
        {
            foreach (DataRow row in dsMain.Tables[CAREER_ADVISOR_TABLE].Rows)
            {
                int advisorID = Convert.ToInt32(row["AdvisorID"]);
                string username = row["Username"].ToString();
                string fullName = row["FullName"].ToString();

                CareerAdvisor advisor = new CareerAdvisor(advisorID, fullName, username);
                advisors.Add(advisor);
            }
        }

        public CareerAdvisor getAdvisor(int advisorID)
        {
            foreach (CareerAdvisor advisor in advisors)
            {
                if (advisor.advisorID == advisorID)
                {
                    return advisor;
                }
            }

            return null;

        }

        #region Database Updates

        private void FillDataRow(DataRow row, CareerAdvisor advisor)
        {
            row["AdvisorID"] = advisor.advisorID;
            row["Username"] = advisor.Username;
            row["FullName"] = advisor.fullName;
        }

        private DataRow FindDataRow(CareerAdvisor advisor)
        {
            for (int i = 0; i < dsMain.Tables[CAREER_ADVISOR_TABLE].Rows.Count; i++)
            {
                if (Convert.ToInt32(dsMain.Tables[CAREER_ADVISOR_TABLE].Rows[i]["AdvisorID"]) == advisor.advisorID)
                {
                    return dsMain.Tables[CAREER_ADVISOR_TABLE].Rows[i];
                }
            }

            return null;
        }

        public void DataSetChange(CareerAdvisor advisor, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsMain.Tables[CAREER_ADVISOR_TABLE].NewRow();
                    FillDataRow(row, advisor);
                    dsMain.Tables[CAREER_ADVISOR_TABLE].Rows.Add(row);

                    advisors.Add(advisor);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(advisor);
                    if (row != null)
                    {
                        FillDataRow(row, advisor);
                    }
                    else
                    {
                        throw new Exception("advisor not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(advisor);
                    if (row != null)
                    {
                        row.Delete();
                        advisors.Remove(advisor);
                    }
                    else
                    {
                        throw new Exception("advisor not found in Tables");
                    }
                    break;
            }
        }

        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            return UpdateDataSource(SQL_SELECT_CAREER_ADVISOR, CAREER_ADVISOR_TABLE);
        }

        internal override void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO Administrator (@AdvisorID, @Username, @FullName) VALUES (AdvisorID, Username, FullName)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE Administrator SET (@AdvisorID, @Username, @FullName) WHERE AdvisorID = @AdvisorID", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM Administrator WHERE AdvisorID = @AdvisorID", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            SqlParameter param = new SqlParameter("@AdvisorID", SqlDbType.Int, 32, "AdvisorID");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Username", SqlDbType.NChar, 255, "Username");
            daMain.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@FullName", SqlDbType.NChar, 255, "FullName");
            daMain.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@AdvisorID", SqlDbType.Int, 32, "AdvisorID");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Username", SqlDbType.NChar, 255, "Username");
            daMain.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@FullName", SqlDbType.NChar, 255, "FullName");
            daMain.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@AdvisorID", SqlDbType.Int, 32, "AdvisorID");
            daMain.DeleteCommand.Parameters.Add(param);
        }

        #endregion


    }

}
