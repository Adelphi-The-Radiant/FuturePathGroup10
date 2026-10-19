using System;
using FuturePathGroup10.Advisory;
using System.Collections.ObjectModel;
using FuturePathGroup10.Data;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

    public class ExportReportDB : DB
    {
        #region Attributes
        private const string TABLE_REPORT = "ExportReport";
        private const string SQL_SELECT_REPORT = "Select * FROM ExportReport";

        public Collection<ExportResult> results { get; private set; }
        private LearnerDB learnerDB;
        #endregion
        public ExportReportDB(LearnerDB learnerDB)
        {
            this.learnerDB = learnerDB;
            results = new Collection<ExportResult>();
            FillDataSet(SQL_SELECT_REPORT, TABLE_REPORT);
            LoadReports();
        }

        private void LoadReports()
        {
            foreach (DataRow row in dsMain.Tables[TABLE_REPORT].Rows)
            {
                int learnerID = Convert.ToInt32(row["LearnerID"]);
                string fullReport = row["FullReport"].ToString();
                string summary = row["Summary"].ToString();

                Learner.Learner learner = learnerDB.getLearner(learnerID);
                ExportResult result = new ExportResult(learner, fullReport, summary);
                results.Add(result);
            }
        }

        #region Update DataSet

        private void FillDataRow(DataRow row, ExportResult result)
        {
            row["LearnerID"] = result.learner.learnerID;
            row["FullReport"] = result.fullReport;
            row["Summary"] = result.summary;
        }

        private DataRow FindDataRow(ExportResult result)
        {
            foreach (DataRow row in dsMain.Tables[TABLE_REPORT].Rows)
            {
                if (Convert.ToInt32(row["LearnerID"]) == result.learner.learnerID)
                {
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(ExportResult result, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsMain.Tables[TABLE_REPORT].NewRow();
                    FillDataRow(row, result);
                    dsMain.Tables[TABLE_REPORT].Rows.Add(row);

                    results.Add(result);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(result);
                    if (row != null)
                    {
                        FillDataRow(row, result);
                    }
                    else
                    {
                        throw new Exception("result not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(result);
                    if (row != null)
                    {
                        row.Delete();
                        results.Remove(result);
                    }
                    else
                    {
                        throw new Exception("result not found in Tables");
                    }
                    break;
            }
        }
        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            return UpdateDataSource(SQL_SELECT_REPORT, TABLE_REPORT);
        }

        internal override void Create_INSERT_Command()
        {
            daMain.InsertCommand = new OleDbCommand("INSERT INTO ExportReport (LearnerID, FullReport, Summary) VALUES (?, ?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daMain.UpdateCommand = new OleDbCommand("UPDATE ExportReport SET FullReport = ?, Summary = ? WHERE LearnerID = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daMain.DeleteCommand = new OleDbCommand("DELETE FROM ExportReport WHERE LearnerID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daMain.InsertCommand.Parameters.Add(CreateParam("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
            daMain.InsertCommand.Parameters.Add(CreateParam("@FullReport", OleDbType.LongVarWChar, 0, "FullReport"));
            daMain.InsertCommand.Parameters.Add(CreateParam("@Summary", OleDbType.LongVarWChar, 0, "Summary"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daMain.UpdateCommand.Parameters.Add(CreateParam("@FullReport", OleDbType.LongVarWChar, 0, "FullReport"));
            daMain.UpdateCommand.Parameters.Add(CreateParam("@Summary", OleDbType.LongVarWChar, 0, "Summary"));
            daMain.UpdateCommand.Parameters.Add(CreateParam("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daMain.DeleteCommand.Parameters.Add(CreateParam("@LearnerID", OleDbType.Integer, 4, "LearnerID"));
        }

        #endregion
    }
}
