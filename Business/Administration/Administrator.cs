using System;
using System.Collections.Generic;
using System.Text;
using FuturePathGroup10.Data;
using FuturePathGroup10.Reccomendation;

namespace FuturePathGroup10.Administration
{
    public class Administrator : AccountHolder
    {
        public int adminID { get;  set; }
        public string fullName { get;  set; }

        public Administrator(int adminID, string fullName,string username) : base(username)
        {
            this.adminID = adminID;
            this.fullName = fullName;
        }

        public void addReferenceData(ReferenceData data)
        {
            try
            {
                data.add();
                MessageBox.Show(fullName + " successfully added new reference data.");
            } catch (Exception e)
            {
                MessageBox.Show("Error adding data: " + e.Message);
            }
        }

        public void updateReferenceData(ReferenceData data)
        {
            try
            {
                data.update();
                MessageBox.Show(fullName + " successfully updated reference data.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error updating data: " + e.Message);
            }
        }
        
        public void removeReferenceData(ReferenceData data)
        {
            try
            {
                data.deactivate();
                MessageBox.Show(fullName + " successfully deleted reference data.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Error removing data: " + e.Message);
            }
        }
    }
}
