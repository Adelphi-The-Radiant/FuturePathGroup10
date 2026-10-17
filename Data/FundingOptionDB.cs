using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using FuturePathGroup10.Funding;


namespace FuturePathGroup10.Data { 

	public class FundingOptionDB : DB
	{
        #region Attributes
        private const string TABLE_FF = "FundingFaculty";
		private const string TABLE_FP = "FundingProgramme";
		private const string TABLE_FO = "FundingOption";
		private const string TABLE_FFONFO = "FFJoinFO";
		private const string TABLE_FPONFO = "FPJoinFO"

        private const string SQL_SELECT_FF = "SELECT * FROM FundingFaculty";
		private const string SQL_SELECT_FP = "SELECT * FROM FundingProgramme";
		private const string SQL_SELECT_FO = "SELECT * FROM FundingOption";
		private const string SQL_SELECT_FFJOINFO = "SELECT * FROM " +
			"(SELECT * FROM FundingOption LEFT JOIN FundingFaculty ON FundingOption.OptionID = FundingFaculty.OptionID " +
			"ORDER BY FundingOption.OptionID) " +
			"AS FFJoinFO";
        private const string SQL_SELECT_FPJOINFO = "SELECT * FROM " +
            "(SELECT * FROM FundingOption LEFT JOIN FundingProgramme ON FundingOption.OptionID = FundingProgramme.OptionID " +
            "ORDER BY FundingOption.OptionID) " +
            "AS FPJoinFO";

		private DataSet dsFF;
		private DataSet dsFP;
		private DataSet dsFO;
		private DataSet dsFFOnFO;
		private DataSet dsFPOnFO;

		private OleDbDataAdapter daFF;
		private OleDbDataAdapter daFP;
		private OleDbDataAdapter daFO;
        private DataSet daFFOnFO;
        private DataSet daFPOnFO;

		//private FacultyDB facultyDB;
		//private DegreeDB degreeDB;

        public Collection<FundingOption> options { get; private set; }
        #endregion

        public FundingOptionDB() //FacultyDB facultyDB,DegreeDB degreeDB
        {
			//this.facultyDB = facultyDB;
			//this.degreeDB = degreeDB;
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
		}

        #region Loadings
        public void LoadFPOnFO()
		{
			int optionID = -1;
			foreach(DataRow row in dsFPOnFO.Tables[TABLE_FPONFO].Rows)
			{
				int candidateID = Convert.ToInt32(row["OptionID"]);

				if (optionID != candidateID)
				{
					optionID = candidateID;
					string description = row["Description"].ToString();
					float amount = Convert.ToDouble(row["Amount"]);
					FundingOption option = new FundingOption(optionID, description, amount);
					options.Add(option);
				}
				//Need some way to get the associated degree give it a funding option
			}

		}

		public void LoadFFOnFO()
		{
            int optionID = -1;
            foreach (DataRow row in dsFPOnFO.Tables[TABLE_FPONFO].Rows)
            {
                int candidateID = Convert.ToInt32(row["OptionID"]);

                if (optionID != candidateID)
                {
                    optionID = candidateID;
                    string description = row["Description"].ToString();
                    float amount = Convert.ToDouble(row["Amount"]);
                    FundingOption option = new FundingOption(optionID, description, amount);
                    options.Add(option);
                }
                //Need some way to get the associated Faculty give it a funding option
            }

        }
        #endregion
    }
