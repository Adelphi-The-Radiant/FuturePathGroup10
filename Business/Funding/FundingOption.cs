using FuturePathGroup10.Data;
using FuturePathGroup10.Prerequisites;
using FuturePathGroup10.Reccomendation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FuturePathGroup10.Funding
{
    public class FundingOption : ReferenceData
    {
        public int optionID { get; private set; }
        public string description { get; private set; }
        public double amount { get; private set; }

        private FundingOptionDB fundingOptionDB;

        public FundingOption (int optionID,string description,double amount, FundingOptionDB fundingOptionDB) : base()
        {
            this.optionID = optionID;
            this.description = description;
            this.amount = amount;
            this.fundingOptionDB = fundingOptionDB;
        }

        public FundingOption(int optionID, string description, double amount) : base()
        {
            this.optionID = optionID;
            this.description = description;
            this.amount = amount;
            this.fundingOptionDB = null;
        }

        public override bool Equals(object? obj)
        {
            if (obj is FundingOption)
            {
                FundingOption other = (FundingOption)obj;
                return optionID == other.optionID;
            }
            return false;
        }
        public string  optionToString()
        {
            return description +": R"+ amount;
        }

        public Collection<string> getFundingOptions()
        {
            throw new NotImplementedException();
        }

        // This takes no parameters and returns a string? Weird for a setter
        public void setFundingOptions()
        {
            throw new NotImplementedException();
        }

        public override void add()
        {
            try
            {
                fundingOptionDB.DataSetChange(this, DB.DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Funding option: " + ex.Message);
            }
        }

        public void add(FundingOptionDB fundingOptionDB)
        {
            try
            {
                fundingOptionDB.DataSetChange(this, DB.DBOperation.Add);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding Funding option: " + ex.Message);
            }
        }

        public override void deactivate()
        {
            try
            {
                fundingOptionDB.DataSetChange(this, DB.DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Funding option: " + ex.Message);
            }
        }

        public void deactivate(FundingOptionDB fundingOptionDB)
        {
            try
            {
                fundingOptionDB.DataSetChange(this, DB.DBOperation.Delete);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting Funding option: " + ex.Message);
            }
        }

        public override void update()
        {
            try
            {
                fundingOptionDB.DataSetChange(this, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Funding Option: " + ex.Message);
            }
        }

        public void update(FundingOptionDB fundingOptionDB)
        {
            try
            {
                fundingOptionDB.DataSetChange(this, DB.DBOperation.Edit);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating Funding Option: " + ex.Message);
            }
        }
    }
}
