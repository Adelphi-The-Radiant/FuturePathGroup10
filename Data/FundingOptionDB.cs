using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using FuturePathGroup10.Funding;
using FuturePathGroup10.Reccomendation;


namespace FuturePathGroup10.Data {

	public class FundingOptionDB : DB
	{
		#region Attributes
		private const string TABLE_FF = "FundingFaculty";
		private const string TABLE_FP = "FundingProgramme";
		private const string TABLE_FO = "FundingOption";
		private const string TABLE_FFONFO = "FFJoinFO";
		private const string TABLE_FPONFO = "FPJoinFO";


		private const string SQL_SELECT_FF = "SELECT * FROM FundingFaculty";
		private const string SQL_SELECT_FP = "SELECT * FROM FundingProgramme";
		private const string SQL_SELECT_FO = "SELECT * FROM FundingOption";
		private const string SQL_SELECT_FFJOINFO = "SELECT FundingOption.OptionID AS OptionID, FundingOption.Description AS Description, FundingOption.Amount AS Amount, FundingFaculty.FacultyID AS FacultyID " +
			"FROM FundingOption LEFT JOIN FundingFaculty ON FundingOption.OptionID = FundingFaculty.OptionID " +
			"ORDER BY FundingOption.OptionID";
		private const string SQL_SELECT_FPJOINFO = "SELECT FundingOption.OptionID AS OptionID, FundingOption.Description AS Description, FundingOption.Amount AS Amount, FundingProgramme.DegreeID AS DegreeID " +
			"FROM FundingOption LEFT JOIN FundingProgramme ON FundingOption.OptionID = FundingProgramme.OptionID " +
			"ORDER BY FundingOption.OptionID";

		private DataSet dsFF;
		private DataSet dsFP;
		private DataSet dsFO;
		private DataSet dsFFOnFO;
		private DataSet dsFPOnFO;

		private OleDbDataAdapter daFF;
		private OleDbDataAdapter daFP;
		private OleDbDataAdapter daFO;
		private OleDbDataAdapter daFFOnFO;
		private OleDbDataAdapter daFPOnFO;

		private FacultyDB facultyDB;
		private DegreeDB degreeDB;

		public Collection<FundingOption> options { get; private set; }
		#endregion

		public FundingOptionDB(FacultyDB facultyDB, DegreeDB degreeDB)
		{
			this.facultyDB = facultyDB;
			this.degreeDB = degreeDB;
			options = new Collection<FundingOption>();

			FillDataSet(SQL_SELECT_FO, TABLE_FO);
			dsFO = dsMain;
			daFO = daMain;

			FillDataSet(SQL_SELECT_FP, TABLE_FP);
			dsFP = dsMain;
			daFP = daMain;

			FillDataSet(SQL_SELECT_FF, TABLE_FF);
			dsFF = dsMain;
			daFF = daMain;

			FillDataSet(SQL_SELECT_FPJOINFO, TABLE_FPONFO);
			dsFPOnFO = dsMain;
			daFPOnFO = daMain;

			FillDataSet(SQL_SELECT_FFJOINFO, TABLE_FFONFO);
			dsFFOnFO = dsMain;
			daFFOnFO = daMain;

			LoadFPOnFO();
			LoadFFOnFO();
		}

		#region Loadings
		private void LoadFPOnFO()
		{
			int optionID = -1;
			FundingOption option = null;
			foreach (DataRow row in dsFPOnFO.Tables[TABLE_FPONFO].Rows)
			{
				int candidateID = Convert.ToInt32(row["OptionID"]);

				if (optionID != candidateID)
				{
					optionID = candidateID;
					string description = row["Description"].ToString();
					double amount = Convert.ToDouble(row["Amount"]);
					option = new FundingOption(optionID, description, amount);
					options.Add(option);
				}
				//Need some way to get the associated degree give it a funding option
				int degreeID = Convert.ToInt32(row["DegreeID"]);
				Degree degree = degreeDB.getDegree(degreeID);
				degree.addFundingOption(option);
			}

		}

		private void LoadFFOnFO()
		{
			int optionID = -1;
            FundingOption option = null;
            foreach (DataRow row in dsFFOnFO.Tables[TABLE_FFONFO].Rows)
			{
				int candidateID = Convert.ToInt32(row["OptionID"]);

				if (optionID != candidateID)
				{
					optionID = candidateID;
					string description = row["Description"].ToString();
					double amount = Convert.ToDouble(row["Amount"]);
					option = new FundingOption(optionID, description, amount);
					FundingOption otherOption = getOption(optionID);
					if (otherOption != null)
					{
						option = otherOption; //Just reuse the reference so we dont have duplicate entries
					}
					else
					{
						options.Add(option);
					}
				}
				//Need some way to get the associated Faculty give it a funding option
				int facultyID = Convert.ToInt32(row["FacultyID"]);
				Faculty faculty = facultyDB.getFaculty(facultyID);
				faculty.addFundingOption(option);

            }

		}

		public FundingOption getOption(int optionID)
		{
			foreach(FundingOption option in options)
			{
				if (option.optionID == optionID)
				{
					return option;
				}
			}
			return null;
		}


        #endregion

        #region DataSet Updates

		private void FillDataRow(DataRow row, FundingOption option)
		{
			row["OptionID"] = option.optionID;
			row["Description"] = option.description;
			row["Amount"] = option.amount;
		}

		private void FillDataRow(DataRow row, FundingOption option, Faculty faculty)
		{
			row["OptionID"] = option.optionID;
			row["FacultyID"] = faculty.facultyID;
        }

        private void FillDataRow(DataRow row, FundingOption option, Degree degree)
        {
            row["OptionID"] = option.optionID;
			row["DegreeID"] = degree.degreeID;
        }

		private DataRow FindDataRow(FundingOption option)
		{
			foreach(DataRow row in dsFO.Tables[TABLE_FO].Rows)
			{
				if (Convert.ToInt32(row["OptionID"]) == option.optionID)
				{
					return row;
				}
			}
			return null;
		}

        private DataRow FindDataRow(FundingOption option, Faculty faculty)
        {
            foreach (DataRow row in dsFF.Tables[TABLE_FF].Rows)
            {
                if (Convert.ToInt32(row["OptionID"]) == option.optionID && Convert.ToInt32(row["FacultyID"]) == faculty.facultyID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDataRow(FundingOption option, Degree degree)
        {
            foreach (DataRow row in dsFP.Tables[TABLE_FP].Rows)
            {
                if (Convert.ToInt32(row["OptionID"]) == option.optionID && Convert.ToInt32(row["DegreeID"]) == degree.degreeID)
                {
                    return row;
                }
            }
            return null;
        }

		public void DataSetChange(FundingOption option, DBOperation operation)
		{
			DataRow row;
			switch (operation)
			{
				case DBOperation.Add:
					row = dsFO.Tables[TABLE_FO].NewRow();
					FillDataRow(row, option);
					dsFO.Tables[TABLE_FO].Rows.Add(row);

					options.Add(option);
					break;

				case DBOperation.Edit:
					row = FindDataRow(option);
					if (row != null)
					{
						FillDataRow(row, option);
					}
					break;
				case DBOperation.Delete:
					row = FindDataRow(option);
                    if (row != null)
                    {
                        row.Delete();
						options.Remove(option);
                    }
                    break;

            }
		}

        public void DataSetChange(FundingOption option, Faculty faculty, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsFF.Tables[TABLE_FF].NewRow();
                    FillDataRow(row, option, faculty);
                    dsFF.Tables[TABLE_FF].Rows.Add(row);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(option, faculty);
                    if (row != null)
                    {
                        FillDataRow(row, option, faculty);
                    }
                    break;
                case DBOperation.Delete:
                    row = FindDataRow(option,faculty);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    break;

            }
        }


        public void DataSetChange(FundingOption option, Degree degree, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsFP.Tables[TABLE_FP].NewRow();
                    FillDataRow(row, option, degree);
                    dsFP.Tables[TABLE_FP].Rows.Add(row);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(option, degree);
                    if (row != null)
                    {
                        FillDataRow(row, option, degree);
                    }
                    break;
                case DBOperation.Delete:
                    row = FindDataRow(option, degree);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    break;

            }
        }
        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            dsMain = dsFO;
            bool check1 = UpdateDataSource(SQL_SELECT_FO, TABLE_FO, daFO);
            dsFO = dsMain;
            dsMain = dsFF;
            bool check2 = UpdateDataSource(SQL_SELECT_FF, TABLE_FF, daFF);
            dsFF = dsMain;
			dsMain = dsFP;
            bool check3 = UpdateDataSource(SQL_SELECT_FP, TABLE_FP, daFP);
			dsFP = dsMain;
            return check1 && check2 && check3;
        }

        internal override void Create_INSERT_Command()
        {
			daFO.InsertCommand = new OleDbCommand("INSERT INTO FundingOption (@OptionID,@Description,@Amount) VALUES (OptionID,Description,Amount)", cnMain);
            daFF.InsertCommand = new OleDbCommand("INSERT INTO FundingFaculty (@OptionID,@FacultyID) VALUES (OptionID,FacultyID)", cnMain);
            daFP.InsertCommand = new OleDbCommand("INSERT INTO FundingProgramme (@OptionID,@DegreeID) VALUES (OptionID,DegreeID)", cnMain);
			Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
			daFO.UpdateCommand = new OleDbCommand("UPDATE FundingOption SET (@OptionID,@Description,@Amount) WHERE OptionID = @OptionID");
			//Not actually possible but whatever.
            daFF.UpdateCommand = new OleDbCommand("UPDATE FundingFaculty SET (@OptionID,@FacultyID) WHERE OptionID = @OptionID AND FacultyID = @FacultyID");
            daFP.UpdateCommand = new OleDbCommand("UPDATE FundingProgramme SET (@OptionID,@DegreeID) WHERE OptionID = @OptionID AND DegreeID = @DegreeID");
			Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daFO.DeleteCommand = new OleDbCommand("DELETE FROM FundingOption WHERE OptionID = @OptionID");
            daFF.DeleteCommand = new OleDbCommand("DELETE FROM FundingFaculty WHERE OptionID = @OptionID AND FacultyID = @FacultyID");
            daFP.DeleteCommand = new OleDbCommand("DELETE FROM FundingProgramme WHERE OptionID = @OptionID AND DegreeID = @DegreeID");
			Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
			SqlParameter param = new SqlParameter("@OptionID", SqlDbType.Int, 4, "OptionID");
			daFO.InsertCommand.Parameters.Add(param);
            daFF.InsertCommand.Parameters.Add(param);
            daFP.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daFF.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daFP.InsertCommand.Parameters.Add(param);

			param = new SqlParameter("@Description", SqlDbType.NChar, 255, "Description");
            daFO.InsertCommand.Parameters.Add(param);

            param = new SqlParameter("@Amount", SqlDbType.Float, 64, "Amount");
            daFO.InsertCommand.Parameters.Add(param);
        }

        internal override void Build_UPDATE_Parameters()
        {
            SqlParameter param = new SqlParameter("@OptionID", SqlDbType.Int, 4, "OptionID");
            daFO.UpdateCommand.Parameters.Add(param);
            daFF.UpdateCommand.Parameters.Add(param);
            daFP.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daFF.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daFP.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Description", SqlDbType.NChar, 255, "Description");
            daFO.UpdateCommand.Parameters.Add(param);

            param = new SqlParameter("@Amount", SqlDbType.Float, 64, "Amount");
            daFO.UpdateCommand.Parameters.Add(param);
        }

        internal override void Build_DELETE_Parameters()
        {
            SqlParameter param = new SqlParameter("@OptionID", SqlDbType.Int, 4, "OptionID");
            daFO.DeleteCommand.Parameters.Add(param);
            daFF.DeleteCommand.Parameters.Add(param);
            daFP.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@FacultyID", SqlDbType.Int, 4, "FacultyID");
            daFF.DeleteCommand.Parameters.Add(param);

            param = new SqlParameter("@DegreeID", SqlDbType.Int, 4, "DegreeID");
            daFP.DeleteCommand.Parameters.Add(param);
        }

        #endregion
    }
}
