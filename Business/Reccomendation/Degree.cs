using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class Degree : ReferenceData
    {
        public int degreeID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }

        // references to DegreeDB and Faculty
        private Data.DegreeDB degreeDB;
        private Faculty faculty; // a degree must have a Faculty

        // constructor
        public Degree(int degreeID, string title, string description, DegreeDB degreeDB, Faculty faculty)
        {
            this.degreeID = degreeID;
            this.title = title;
            this.description = description;

            this.degreeDB = degreeDB;
            this.faculty = faculty;
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
