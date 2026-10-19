using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using FuturePathGroup10.Administration;
using FuturePathGroup10.Data;
using FuturePathGroup10.Reccomendation;

namespace FuturePathGroup10.Administration
{
    internal class AdministratorController
    {
        // references to Administrator, DegreeDB, and FacultyDB
        private Administrator admin;
        private DegreeDB degreeDB;
        private FacultyDB facultyDB;

        public LearnerDB learnerDB;
        // List of the reference data for the UI
        private List<ReferenceData> referenceDatas;

        // constructor
        public AdministratorController(Administrator admin, DegreeDB degreeDB, FacultyDB facultyDB)
        {
            this.admin = admin;
            this.degreeDB = degreeDB;
            this.facultyDB = facultyDB;
            this.referenceDatas = new List<ReferenceData>();
            this.learnerDB = new LearnerDB();
        }

        // this retrieves all current faculties
        public void selectMaintenance()
        {
            // reset old list of faculties (in case it has been updated)
            referenceDatas.Clear();
            // add the new faculties
            foreach (var faculty in facultyDB.faculties)
            {
                referenceDatas.Add(faculty);
            }
        }

        // no idea what to do here... no history table :P
        public void selectHistory()
        {
            throw new NotImplementedException();
        }

        // gets a list of all things that need to be done and does it all
        public void generateUpdateList()
        {
            try
            {
                // whether the updates were a success for each table
                bool degree = degreeDB.UpdateAllSources();
                bool faculty = facultyDB.UpdateAllSources();

                if (degree && faculty)
                {
                    MessageBox.Show("Successfully updated the database!");
                } else
                {
                    MessageBox.Show("Some updates failed.");
                }
            } catch (Exception ex)
            {
                MessageBox.Show("Error updating: " +  ex.Message);
            }
        }
        public Collection<Learner.Learner> generateComboBox() { 
            return learnerDB.learners;
        }
    }
}
