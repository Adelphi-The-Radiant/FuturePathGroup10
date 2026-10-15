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
        private static string dbPath = Path.Combine(AppContext.BaseDirectory, "Data", "FuturePath.accdb");
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
            try
            {
                daMain = new OleDbDataAdapter(aSQLstring, cnMain);
                cnMain.Open();
                daMain.Fill(dsMain, aTable);
                cnMain.Close();
            }
            catch (Exception errObj)
            {
                MessageBox.Show(errObj.Message + "  " + errObj.StackTrace);
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
                FillDataSet(sqlLocal, table);
                success = true;
            }
            catch (Exception errObj)
            {
                MessageBox.Show(errObj.Message + " " + errObj.StackTrace);
                success = false;
            }
            finally
            {
            }
            return success;    
        }
        internal bool UpdateDataSource(string sqlLocal, string table)
        {
            return UpdateDataSource(sqlLocal, table, daMain);
        }
        #endregion
        
        #region Abstract CRUD
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
        abstract internal void Create_INSERT_Command();
        abstract internal void Create_UPDATE_Command();
        abstract internal void Create_DELETE_Command();
        
        abstract internal void Build_INSERT_Parameters();
        abstract internal void Build_UPDATE_Parameters();
        abstract internal void Build_DELETE_Parameters();
        
        abstract public  bool UpdateAllSources();

        #endregion


    }
}
