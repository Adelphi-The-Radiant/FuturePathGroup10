using FuturePathGroup10.Funding;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FuturePathGroup10.Data;

namespace FuturePathGroup10.Reccomendation
{
    public class Faculty : ReferenceData
    {
        public int facultyID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<FundingOption> options { get; private set; }
        public Collection<Degree> degreesOffered { get; private set; }

        private FacultyDB facultyDB;
        private EnrollmentList enrollmentList;

        // constructor
        public Faculty(int facultyID, string title, string description, FacultyDB facultyDB)
        {
            this.facultyID = facultyID;
            this.description = description;
            this.title = title;
            options = new Collection<FundingOption>();
            degreesOffered = new Collection<Degree>();
            this.facultyDB = facultyDB;
        }

        public void addFundingOption(FundingOption option)
        {
            if (option != null)
            {
                options.Add(option);
            }
        }

        public void addDegree(Degree degree)
        {
            if (degree != null)
            {
                degreesOffered.Add(degree);
            }
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
                facultyDB.DataSetChange(el, DB.DBOperation.Add);
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
                facultyDB.DataSetChange(el, DB.DBOperation.Delete);
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
                facultyDB.DataSetChange(el, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Faculty: " + ex.Message);
            }
        }
    }
}
