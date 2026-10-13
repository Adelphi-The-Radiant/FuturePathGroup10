using System;
using System.Data;
using System.Data.OleDb;

namespace FuturePathGroup10.Data
{
    public class DB
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

        protected bool UpdateDataSource(string sqlLocal, string table, OleDbDataAdapter DA)
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
        protected bool UpdateDataSource(string sqlLocal, string table)
        {
            return UpdateDataSource(sqlLocal, table, daMain);
        }
        #endregion

        
    }
}
