using FuturePathGroup10.Business;
using FuturePathGroup10.Data;
using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FuturePathGroup10.Business
{
    internal class LearnerController
    {
        #region Data Members
        public LearnerDB learnerDB;

        public Faculty SelectedFaculty { get; private set; }

        public DegreeProgramme Programme { get; private set; }  

        public Learner activeLearner { get; private set; }
        #endregion

        #region Constructor
        public LearnerController() { 
            this.learnerDB =  new LearnerDB();
            this.activeLearner = null;
        }
        #endregion

        #region Methods 
        public bool createProfile(Learner learner, out string error)
        {
            error = Validate(learner);                    //validates mandatory fields
            if (error != "") return false;
            learner.learnerID = learnerDB.Add(learner);
            return true;

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

            try { learnerDB.Update(learner); }
            catch (Exception ex) { error = "Could not save: " + ex.Message; return false; }
            activeLearner = learner;
            return true;
        }

        // Helper method to validate if the mandotory fileds have information profile creation and update
        private string Validate(Learner l)
        {
            if (string.IsNullOrWhiteSpace(l.school)) return "School is required.";
            if (string.IsNullOrWhiteSpace(l.province)) return "Province is required.";
            return "";   // empty string means valid
        }



        public bool selectFaculty(int facultyId, out string error)
        {
            error = "";
            Faculty faculty = facultyDB.GetById(facultyId);      // delegates to the data class
            if (faculty == null )
            {
                error = "Please choose a valid faculty.";
                return false;
            }

            //SelectedFaculty = faculty;                            
            return true;
        }

        public bool selectDegreeProgramme(int degreeProgrammeId, out string error)
        {
            error = "";
            DegreeProgramme programme = degreeProgrammeDB.getById (degreeProgrammeId);
            
            if (programme == null )
            {
                error = "Please choose a valid degree programme.";
                return false;
            }

            Programme = programme;
            return true;
        }


        public Collection<string> viewFundingOption(FundingOption options)
        {
            return options.getFundingOptions();
        }

        public bool checkEligibility(Learner learner) {

            ProgrammeRequirement p = new ProgrammeRequirement(Programme);
            return p.compareMarks();
        }
        #endregion
    }
}
