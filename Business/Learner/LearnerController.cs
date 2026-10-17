using FuturePathGroup10.Data;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static FuturePathGroup10.Data.DB;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FuturePathGroup10.Learner
{
    internal class LearnerController
    {
        #region Data Members
        public LearnerDB learnerDB;

        public SubjectDB subjectDB;

        public Faculty SelectedFaculty { get; private set; }

        public DegreeProgramme Programme { get; private set; }

        public Learner activeLearner { get; private set; }
        #endregion

        #region Constructor
        public LearnerController()
        {
            this.learnerDB = new LearnerDB();
            this.subjectDB = new SubjectDB(learnerDB);
            this.activeLearner = null;
        }
        #endregion

        #region Methods 

        public bool createProfile(Learner learner, out string error)
        {
            error = Validate(learner);

            if (error != "")
                return false;

            try
            {
                learnerDB.DataSetChange(learner, DB.DBOperation.Add);
                activeLearner = learner;
                return true;
            }
            catch (Exception ex)
            {
                error = "Could not create profile: " + ex.Message;
                return false;
            }
        }

        public bool updateProfile(Learner learner, out string error)
        {
            error = Validate(learner);
            if (error != "") return false;

            //additional mandatory field for an already existing learner
            if (learner.learnerID <= 0)
            {
                error = "This learner has not been created yet.";
                return false;
            }

            try { learnerDB.DataSetChange(learner, DB.DBOperation.Edit); }
            catch (Exception ex) { error = "Could not save: " + ex.Message; return false; }
            activeLearner = learner;
            return true;
        }

        // Helper method to validate if the mandotory fileds have information profile creation and update
        // Should add more if not all idk :(
        private string Validate(Learner l)
        {
            if (string.IsNullOrWhiteSpace(l.school)) return "School is required.";
            if (string.IsNullOrWhiteSpace(l.province)) return "Province is required.";
            return "";   // empty string means valid
        }



        public bool selectFaculty(Reccomendation.Faculty faculty, out string error)
        {
            error = "";

            if (faculty == null)
            {
                error = "Please choose a valid faculty.";
                return false;
            }

            SelectedFaculty = faculty;
            return true;
        }

        public bool selectDegreeProgramme(Reccomendation.DegreeProgramme programme, out string error)
        {
            error = "";

            if (programme == null)
            {
                error = "Please choose a valid degree programme.";
                return false;
            }

            Programme = programme;
            return true;
        }

        public void captureMarks(Dictionary <Subject, double> marks) 
        {
            foreach (KeyValuePair<Subject, double> entry in marks)
            {
                Subject subject = entry.Key;
                double mark = entry.Value;

                Prerequisites.LearnerSubject eachSubject = new LearnerSubject(activeLearner, subject, mark);
                subjectDB.DataSetChange(eachSubject, DBOperation.Add);
            }


            }


        }
        #endregion
    }
