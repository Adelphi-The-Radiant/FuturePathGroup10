using FuturePathGroup10.Funding;
using FuturePathGroup10.Interest;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Data;

namespace FuturePathGroup10.Reccomendation
{
    public class Degree : ReferenceData
    {
        public int degreeID { get; private set; }
        public string title { get; private set; }
        public string description { get; private set; }
        public Collection<FundingOption> options { get; private set; }

        public Collection<InterestCategory> categories { get; private set; } //a list of category this degree maps to

        public Collection<ProgrammeRequirement> requirements { get; private set; }

        // references to DegreeDB and Faculty
        private Data.DegreeDB degreeDB;
        private Faculty faculty; // a degree must have a Faculty

        // constructor
        public Degree(int degreeID, string title, string description)
        {
            this.degreeID = degreeID;
            this.title = title;
            this.description = description;
            options = new Collection<FundingOption>();
            categories = new Collection<InterestCategory>();
            requirements = new Collection<ProgrammeRequirement>();
        }

        public void addFundingOption(FundingOption option)
        {
            if (option != null)
            {
                options.Add(option);
            }
        }

        public void addRequirement(ProgrammeRequirement requirement)
        {
            if (requirement != null)
            {
                requirements.Add(requirement);
            }
        }

        public Degree GetDegree()
        {
            return (Degree)this;
        }
        public void addCategory(InterestCategory category)
        {
            if (!categories.Contains(category))
            {
                categories.Add(category);
            }
        }

        public override void add()
        {
            try
            {
                degreeDB.DataSetChange(this,faculty, DB.DBOperation.Add);
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
                degreeDB.DataSetChange(this,faculty, DB.DBOperation.Delete);
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
                degreeDB.DataSetChange(this, faculty, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Degree: " + ex.Message);
            }
        }
    }
}
