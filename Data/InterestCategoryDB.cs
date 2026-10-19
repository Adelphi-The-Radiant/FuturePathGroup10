using FuturePathGroup10.Reccomendation;
using FuturePathGroup10.Interest;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace FuturePathGroup10.Data
{

    public class InterestCategoryDB : DB
    {
        #region Attributes

        private const string SQL_SELECT_JOINED = "SELECT InterestCategory.InterestCategoryID AS InterestCategoryID, InterestCategory.Title AS Title, InterestCategory.Description AS Description, QuestionCategoryMapping.QuestionID AS QuestionID " +
            "FROM InterestCategory LEFT JOIN QuestionCategoryMapping ON InterestCategory.InterestCategoryID = QuestionCategoryMapping.InterestCategoryID " +
            "ORDER BY InterestCategory.InterestCategoryID";
        private const string TABLE_JOINED = "JoinedTable";
        private const string TABLE_QCM = "QuestionCategoryMapping";
        private const string TABLE_CATEGORY = "InterestCategory";
        private const string TABLE_DIM = "DegreeInterestMapping";
        private const string SQL_SELECT_QCM = "SELECT * FROM QuestionCategoryMapping";
        private const string SQL_SELECT_CATEGORY = "SELECT * FROM InterestCategory";
        private const string SQL_SELECT_DIM = "SELECT * FROM DegreeInterestMapping";

        private DataSet dsQCM;
        private DataSet dsCategory;
        private DataSet dsDIM;

        private OleDbDataAdapter daQCM;
        private OleDbDataAdapter daCategory;
        private OleDbDataAdapter daDIM;
        private InterestQuestionnaireDB IQDB;
        private DegreeDB degreeDB;

        public Collection<InterestCategory> categories { get; private set; }

        #endregion
        public InterestCategoryDB(InterestQuestionnaireDB IQDB, DegreeDB degreeDB)
        {
            this.IQDB = IQDB;
            this.degreeDB = degreeDB;
            categories = new Collection<InterestCategory>();

            FillDataSet(SQL_SELECT_QCM, TABLE_QCM);
            dsQCM = dsMain;
            daQCM = daMain;

            FillDataSet(SQL_SELECT_CATEGORY, TABLE_CATEGORY);
            dsCategory = dsMain;
            daCategory = daMain;

            FillDataSet(SQL_SELECT_JOINED, TABLE_JOINED);

            LoadCategories();

            FillDataSet(SQL_SELECT_DIM, TABLE_DIM);
            dsDIM = dsMain;
            daDIM = daMain;
            LoadDegrees();
        }
        #region getters
        public InterestCategory getInterestCategory(int interestCategoryID)
        {
            foreach (InterestCategory category in categories)
            {
                if (category.interestCategoryID == interestCategoryID)
                {
                    return category;
                }
            }
            return null;
        }

        #endregion
        #region Loading
        private void LoadCategories()
        {
            int interestCategoryID = -1;
            InterestCategory category = null;

            foreach (DataRow row in dsMain.Tables[TABLE_JOINED].Rows)
            {
                int candidateID = Convert.ToInt32(row["InterestCategoryID"]);

                if (interestCategoryID != candidateID)
                {
                    interestCategoryID = candidateID;
                    string title = row["Title"].ToString();
                    string description = row["Description"].ToString();

                    category = new InterestCategory(interestCategoryID, title, description);
                    categories.Add(category);
                }

                if (row["QuestionID"] is DBNull) continue;

                InterestQuestion question = IQDB.getQuestion(Convert.ToInt32(row["QuestionID"]));
                question?.addCategory(category);
            }
        }

        private void LoadDegrees()
        {
            foreach (DataRow row in dsMain.Tables[TABLE_DIM].Rows)
            {
                InterestCategory category = getInterestCategory(Convert.ToInt32(row["InterestCategoryID"]));
                Degree degree = degreeDB.getDegree(Convert.ToInt32(row["DegreeID"]));

                if (category == null || degree == null) continue;

                category.addDegree(degree);
                degree.addCategory(category);
            }
        }
        #endregion

        #region Update DataSet

        private void FillDataRow(DataRow row, InterestCategory category)
        {
            row["InterestCategoryID"] = category.interestCategoryID;
            row["Title"] = category.title;
            row["Description"] = category.description;
        }

        private void FillDataRow(DataRow row, InterestCategory category, InterestQuestion question)
        {
            row["InterestCategoryID"] = category.interestCategoryID;
            row["QuestionID"] = question.questionID;
        }

        private void FillDataRow(DataRow row, InterestCategory category, Degree degree)
        {
            row["InterestCategoryID"] = category.interestCategoryID;
            row["DegreeID"] = degree.degreeID;
        }

        private DataRow FindDataRow(InterestCategory category)
        {
            foreach (DataRow row in dsCategory.Tables[TABLE_CATEGORY].Rows)
            {
                if (Convert.ToInt32(row["InterestCategoryID"]) == category.interestCategoryID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDataRow(InterestCategory category, InterestQuestion question)
        {
            foreach (DataRow row in dsQCM.Tables[TABLE_QCM].Rows)
            {
                if (Convert.ToInt32(row["InterestCategoryID"]) == category.interestCategoryID && Convert.ToInt32(row["QuestionID"]) == question.questionID)
                {
                    return row;
                }
            }
            return null;
        }

        private DataRow FindDataRow(InterestCategory category, Degree degree)
        {
            foreach (DataRow row in dsDIM.Tables[TABLE_DIM].Rows)
            {
                if (Convert.ToInt32(row["InterestCategoryID"]) == category.interestCategoryID && Convert.ToInt32(row["DegreeID"]) == degree.degreeID)
                {
                    return row;
                }
            }
            return null;
        }

        public void DataSetChange(InterestCategory category, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsCategory.Tables[TABLE_CATEGORY].NewRow();
                    FillDataRow(row, category);
                    dsCategory.Tables[TABLE_CATEGORY].Rows.Add(row);

                    categories.Add(category);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(category);
                    if (row != null)
                    {
                        FillDataRow(row, category);
                    }
                    else
                    {
                        throw new Exception("category not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(category);
                    if (row != null)
                    {
                        row.Delete();
                        categories.Remove(category);
                    }
                    else
                    {
                        throw new Exception("category not found in Tables");
                    }
                    break;
            }
        }

        public void DataSetChange(InterestCategory category, InterestQuestion question, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsQCM.Tables[TABLE_QCM].NewRow();
                    FillDataRow(row, category, question);
                    dsQCM.Tables[TABLE_QCM].Rows.Add(row);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(category, question);
                    if (row != null)
                    {
                        FillDataRow(row, category, question);
                    }
                    else
                    {
                        throw new Exception("category or question not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(category, question);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    else
                    {
                        throw new Exception("category or question not found in Tables");
                    }
                    break;
            }
        }

        public void DataSetChange(InterestCategory category, Degree degree, DBOperation operation)
        {
            DataRow row;
            switch (operation)
            {
                case DBOperation.Add:
                    row = dsDIM.Tables[TABLE_DIM].NewRow();
                    FillDataRow(row, category, degree);
                    dsDIM.Tables[TABLE_DIM].Rows.Add(row);
                    break;

                case DBOperation.Edit:
                    row = FindDataRow(category, degree);
                    if (row != null)
                    {
                        FillDataRow(row, category, degree);
                    }
                    else
                    {
                        throw new Exception("category or degree not found in Tables");
                    }
                    break;

                case DBOperation.Delete:
                    row = FindDataRow(category, degree);
                    if (row != null)
                    {
                        row.Delete();
                    }
                    else
                    {
                        throw new Exception("category or degree not found in Tables");
                    }
                    break;
            }
        }
        #endregion

        #region CRUD

        internal override bool UpdateAllSources()
        {
            dsMain = dsCategory;
            bool check1 = UpdateDataSource(SQL_SELECT_CATEGORY, TABLE_CATEGORY, daCategory);
            dsCategory = dsMain;
            dsMain = dsQCM;
            bool check2 = UpdateDataSource(SQL_SELECT_QCM, TABLE_QCM, daQCM);
            dsQCM = dsMain;
            dsMain = dsDIM;
            bool check3 = UpdateDataSource(SQL_SELECT_DIM, TABLE_DIM, daDIM);
            dsDIM = dsMain;
            return check1 && check2 && check3;
        }

        internal override void Create_INSERT_Command()
        {
            daCategory.InsertCommand = new OleDbCommand("INSERT INTO InterestCategory (InterestCategoryID, Title, Description) VALUES (?, ?, ?)", cnMain);
            daQCM.InsertCommand = new OleDbCommand("INSERT INTO QuestionCategoryMapping (QuestionID, InterestCategoryID) VALUES (?, ?)", cnMain);
            daDIM.InsertCommand = new OleDbCommand("INSERT INTO DegreeInterestMapping (DegreeID, InterestCategoryID) VALUES (?, ?)", cnMain);
            Build_INSERT_Parameters();
        }

        internal override void Create_UPDATE_Command()
        {
            daCategory.UpdateCommand = new OleDbCommand("UPDATE InterestCategory SET Title = ?, Description = ? WHERE InterestCategoryID = ?", cnMain);
            daQCM.UpdateCommand = new OleDbCommand("UPDATE QuestionCategoryMapping SET QuestionID = ? WHERE QuestionID = ? AND InterestCategoryID = ?", cnMain);
            daDIM.UpdateCommand = new OleDbCommand("UPDATE DegreeInterestMapping SET DegreeID = ? WHERE DegreeID = ? AND InterestCategoryID = ?", cnMain);
            Build_UPDATE_Parameters();
        }

        internal override void Create_DELETE_Command()
        {
            daCategory.DeleteCommand = new OleDbCommand("DELETE FROM InterestCategory WHERE InterestCategoryID = ?", cnMain);
            daQCM.DeleteCommand = new OleDbCommand("DELETE FROM QuestionCategoryMapping WHERE QuestionID = ? AND InterestCategoryID = ?", cnMain);
            daDIM.DeleteCommand = new OleDbCommand("DELETE FROM DegreeInterestMapping WHERE DegreeID = ? AND InterestCategoryID = ?", cnMain);
            Build_DELETE_Parameters();
        }

        internal override void Build_INSERT_Parameters()
        {
            daCategory.InsertCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));
            daCategory.InsertCommand.Parameters.Add(CreateParam("@Title", OleDbType.VarWChar, 255, "Title"));
            daCategory.InsertCommand.Parameters.Add(CreateParam("@Description", OleDbType.LongVarWChar, 0, "Description"));

            daQCM.InsertCommand.Parameters.Add(CreateParam("@QuestionID", OleDbType.Integer, 4, "QuestionID"));
            daQCM.InsertCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));

            daDIM.InsertCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
            daDIM.InsertCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));
        }

        internal override void Build_UPDATE_Parameters()
        {
            daCategory.UpdateCommand.Parameters.Add(CreateParam("@Title", OleDbType.VarWChar, 255, "Title"));
            daCategory.UpdateCommand.Parameters.Add(CreateParam("@Description", OleDbType.LongVarWChar, 0, "Description"));
            daCategory.UpdateCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));

            daQCM.UpdateCommand.Parameters.Add(CreateParam("@QuestionID", OleDbType.Integer, 4, "QuestionID"));
            daQCM.UpdateCommand.Parameters.Add(CreateParam("@QuestionID", OleDbType.Integer, 4, "QuestionID"));
            daQCM.UpdateCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));

            daDIM.UpdateCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
            daDIM.UpdateCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
            daDIM.UpdateCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));
        }

        internal override void Build_DELETE_Parameters()
        {
            daCategory.DeleteCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));

            daQCM.DeleteCommand.Parameters.Add(CreateParam("@QuestionID", OleDbType.Integer, 4, "QuestionID"));
            daQCM.DeleteCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));

            daDIM.DeleteCommand.Parameters.Add(CreateParam("@DegreeID", OleDbType.Integer, 4, "DegreeID"));
            daDIM.DeleteCommand.Parameters.Add(CreateParam("@InterestCategoryID", OleDbType.Integer, 4, "InterestCategoryID"));
        }

        #endregion
    }
}
