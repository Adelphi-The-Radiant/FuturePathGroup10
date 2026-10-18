using FuturePathGroup10.Data;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Interest
{
    public class InterestCategory : ReferenceData
    {
        public int interestCategoryID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<Degree> degrees { get; private set; }

        private InterestCategoryDB interestCategoryDB;

        public InterestCategory(int interestCategoryID,string title,string description)
        {
            this.interestCategoryID = interestCategoryID;
            this.title = title;
            this.description = description;
            degrees = new Collection<Degree>();
        }

        public void addDegree(Degree degree)
        {
            if (degree != null)
            {
                degrees.Add(degree);
            }
        }

        public override void add()
        {
            try
            {
                interestCategoryDB.DataSetChange(this, DB.DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Interest Category: " + ex.Message);
            }
        }

        public override void deactivate()
        {
            try
            {
                interestCategoryDB.DataSetChange(this, DB.DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Interest Category: " + ex.Message);
            }
        }

        public override void update()
        {
            try
            {
                interestCategoryDB.DataSetChange(this, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Interest Category: " + ex.Message);
            }
        }
    }
}
