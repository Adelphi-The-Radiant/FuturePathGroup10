using System;
using System.Data;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FuturePathGroup10.Data
{
    public abstract class DB
    {
        #region Variable declaration
        private static string dbPath = Path.Combine(AppContext.BaseDirectory, @"..", @"..", @"..", "Data", "FuturePath.accdb");
        private static string strConn = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + dbPath + ";";
        protected OleDbConnection cnMain;
        protected DataSet dsMain;
        protected OleDbDataAdapter daMain;

        public enum DBOperation
        {
            Add,
            Edit,
            Delete
        }
        #endregion

        #region Constructor
        public DB()
        {
            try
            {
                //Open a connection & create a new dataset object
                cnMain = new OleDbConnection(strConn);
                dsMain = new DataSet();
            }
            catch (SystemException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Error");
                return;
            }
        }

        #endregion

        #region Update the DateSet
        public void FillDataSet(string aSQLstring, string aTable)
        {
            daMain = new OleDbDataAdapter(aSQLstring, cnMain);
            try
            {
                cnMain.Open();
                daMain.Fill(dsMain, aTable);
            }
            catch (Exception)
            {
                // Ensure connection is closed and rethrow so callers can react to the failure
                if (cnMain.State == ConnectionState.Open)
                    cnMain.Close();
                throw;
            }
            finally
            {
                if (cnMain.State == ConnectionState.Open)
                    cnMain.Close();
            }
        }

        #endregion

        #region Update the data source

        internal bool UpdateDataSource(string sqlLocal, string table, OleDbDataAdapter DA)
        {
            bool success;
            try
            {
                //open the connection
                cnMain.Open();
                //***update the database table via the data adapter
                DA.Update(dsMain, table);
                //---close the connection
                cnMain.Close();
                //refresh the dataset
                dsMain.Tables[table].Clear();
                FillDataSet(sqlLocal, table);
                success = true;
            }
            catch (Exception errObj)
            {
                if (cnMain.State == ConnectionState.Open)
                    cnMain.Close();
                MessageBox.Show("Could not save changes to the " + table + " table: " + errObj.Message + DescribeSaveFailure(table));
                success = false;
            }
            finally
            {
            }
            return success;
        }

        private string DescribeSaveFailure(string table)
        {
            System.Text.StringBuilder detail = new();
            try
            {
                DataTable? pending = dsMain.Tables[table].GetChanges(DataRowState.Added);
                if (pending != null)
                {
                    detail.AppendLine().AppendLine().Append("Rows being inserted: ");
                    foreach (DataRow row in pending.Rows)
                    {
                        detail.Append("(").Append(string.Join(", ", row.ItemArray)).Append(") ");
                    }
                }
            }
            catch (Exception) { }

            try
            {
                cnMain.Open();
                DataTable indexes = cnMain.GetOleDbSchemaTable(OleDbSchemaGuid.Indexes, new object[] { null, null, null, null, table });
                detail.AppendLine().AppendLine().Append("Unique indexes on " + table + ": ");
                foreach (DataRow index in indexes.Rows)
                {
                    bool isPrimary = index["PRIMARY_KEY"] is bool p && p;
                    bool isUnique = index["UNIQUE"] is bool u && u;
                    if (isPrimary || isUnique)
                    {
                        detail.Append(index["INDEX_NAME"]).Append(isPrimary ? " [primary key]" : " [unique]").Append(" on ").Append(index["COLUMN_NAME"]).Append("; ");
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                if (cnMain.State == ConnectionState.Open) cnMain.Close();
            }
            return detail.ToString();
        }

        internal bool UpdateDataSource(string sqlLocal, string table)
        {
            return UpdateDataSource(sqlLocal, table, daMain);
        }
        #endregion

        #region Abstract CRUD

        internal static OleDbParameter CreateParam(string name, OleDbType type, int size, string sourceColumn)
        {
            return new OleDbParameter(name, type, size, sourceColumn);
        }

        //Important! Required to update the real database with all changes!!!
        public bool UpdateDataSource(DBOperation operation)
        {
            switch (operation)
            {
                case DBOperation.Add:
                    Create_INSERT_Command();
                    break;
                case DBOperation.Edit:
                    Create_UPDATE_Command();
                    break;
                case DBOperation.Delete:
                    Create_DELETE_Command();
                    break;
            }
            return UpdateAllSources();
        }

        public bool RecklessUpdate()
        {
            Create_UPDATE_Command();
            Create_DELETE_Command();
            Create_INSERT_Command();
            return UpdateAllSources();
        }
        abstract internal void Create_INSERT_Command();
        abstract internal void Create_UPDATE_Command();
        abstract internal void Create_DELETE_Command();

        abstract internal void Build_INSERT_Parameters();
        abstract internal void Build_UPDATE_Parameters();
        abstract internal void Build_DELETE_Parameters();

        //Have this inbetween step incase there are multiple DBs that need to be updated
        abstract internal bool UpdateAllSources();

        #endregion


    }
}
