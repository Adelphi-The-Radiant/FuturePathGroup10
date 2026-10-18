using FuturePathGroup10.Data;
using FuturePathGroup10.Funding;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Interest
{
    public class InterestQuestion : ReferenceData
    {
        public int questionID { get; private set; }
        public int interestCategoryID { get; private set; }
        public string question { get; private set; }
        public Collection<InterestCategory> categories { get; private set; }

        private InterestQuestionnaireDB InterestQuestionnaireDB;
        public InterestQuestion(int questionID, int interestCategoryID, string question, InterestQuestionnaireDB interestQuestionnaireDB)
        {
            this.questionID = questionID;
            this.interestCategoryID = interestCategoryID;
            this.question = question;
            categories = new Collection<InterestCategory>();
            this.InterestQuestionnaireDB = interestQuestionnaireDB;
        }

        public void addCategory(InterestCategory category)
        {
            categories.Add(category);
        }

        public void responseToCategory(string response)
        {
            if (string.IsNullOrEmpty(response)) return;

            foreach (var category in categories)
            {

            }
        }

        public override void add()
        {
            try
            {
                InterestQuestionnaireDB.DataSetChange(this, DB.DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Question: " + ex.Message);
            }
        }

        public override void deactivate()
        {
            try
            {
                InterestQuestionnaireDB.DataSetChange(this, DB.DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Question: " + ex.Message);
            }
        }

        public override void update()
        {
            try
            {
                InterestQuestionnaireDB.DataSetChange(this, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Question: " + ex.Message);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is InterestQuestion)
            {
                InterestQuestion other = (InterestQuestion) obj;
                return questionID == other.questionID;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return question.GetHashCode();
        }
    }
}
