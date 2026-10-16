using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Administration
{
    public class Administrator : AccountHolder
    {
        public int adminID { get; private set; }
        public string fullName { get; private set; }

        public Administrator(int adminID, string fullName,string username) : base(username)
        {
            this.adminID = adminID;
            this.fullName = fullName;
        }

        public void addReferenceData(ReferenceData data)
        {
            throw new NotImplementedException();
        }

        public void updateReferenceData(ReferenceData data)
        {
            throw new NotImplementedException();
        }
        
        public void removeReferenceData(ReferenceData data)
        {
            throw new NotImplementedException();
        }
    }
}
