using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10
{
    // AuthController stereotype
    internal class UserAccount
    {
        private string username;
        private string password;
        // Should be a more specialized type for dates
        private int dateCreated;
        private string permissionLevel;

        public void login(string username, string password)
        {
            throw new NotImplementedException();
        }

        public void register(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
