using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Reccomendation
{
    public class Faculty : ReferenceData
    {
        public int facultyID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }

        private Data.FacultyDB facultyDB;
        private EnrollmentList enrollmentList;

        // constructor
        public Faculty(int facultyID, string title, string description, FacultyDB facultyDB, EnrollmentList enrollmentList)
        {
            this.facultyID = facultyID;
            this.description = description;
            this.title = title;

            this.facultyDB = facultyDB;
            this.enrollmentList = enrollmentList; // PLEASE CHECK!!!!
            // shouldthis.enrollmentList = new EnrollmentList(learner???, this, DateTime.Now)
            // please check if this should be implemnented instead??? im not sure how an EnrollmentList will be created elsewhere
        }

        public Faculty getFaculty()
        {
            return (Faculty)this;
        }

        public override void add()
        {
            try
            {
                EnrollmentList el = new EnrollmentList(null, this, DateTime.Now);
                facultyDB.DataSetChange(el, DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Faculty: " + ex.Message);
            }
        }

        public override void deactivate()
        {
            try
            {
                EnrollmentList el = new EnrollmentList(null, this, DateTime.Now);
                facultyDB.DataSetChange(el, DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Faculty: " + ex.Message);
            }
        }

        public override void update()
        {
            try
            {
                EnrollmentList el = new EnrollmentList(null, this, DateTime.Now);
                facultyDB.DataSetChange(el, DBOperation.Update);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Faculty: " + ex.Message);
            }
        }
    }
}
