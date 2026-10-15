using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10.Administration
{
    internal class Administrator : AccountHolder
    {
        private int adminID;
        private string username;
        private string fullName;

        public Administrator(string username) : base(username)
        {

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
