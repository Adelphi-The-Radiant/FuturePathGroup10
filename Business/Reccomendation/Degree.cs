using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Funding;
using System.Collections.ObjectModel;

namespace FuturePathGroup10.Reccomendation
{
    public class Degree : ReferenceData
    {
        public int degreeID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<FundingOption> options { get; private set; }

        // references to DegreeDB and Faculty
        private Data.DegreeDB degreeDB;
        private Faculty faculty; // a degree must have a Faculty

        // constructor
        public Degree(int degreeID, string title, string description, DegreeDB degreeDB, Faculty faculty)
        {
            this.degreeID = degreeID;
            this.title = title;
            this.description = description;
            options = new Collection<FundingOption>();
        }

        public void addFundingOption(FundingOption option)
        {
            if (option != null)
            {
                options.Add(option);
            }
        }

        public Degree GetDegree()
        {
            return (Degree)this;
        }

        public override void add()
        {
            try
            {
                // creating a DegreeProgramme object (DegreeDB uses DegreeProgramme)
                DegreeProgramme dp = new DegreeProgramme(faculty, this);
                degreeDB.DataSetChange(dp, DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Degree: " + ex.Message);
            }
        }

        public override void deactivate()
        {
            try
            {
                // creating a DegreeProgramme object (DegreeDB uses DegreeProgramme)
                DegreeProgramme dp = new DegreeProgramme(faculty, this);
                degreeDB.DataSetChange(dp, DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Degree: " + ex.Message);
            }
        }

        public override void update()
        {
            try
            {
                // creating a DegreeProgramme object (DegreeDB uses DegreeProgramme)
                DegreeProgramme dp = new DegreeProgramme(faculty, this);
                degreeDB.DataSetChange(dp, DBOperation.Update);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Degree: " + ex.Message);
            }
        }
    }
}
