using System;
using System.Collections.Generic;
using System.Text;

namespace FuturePathGroup10
{
    // AuthController stereotype
    public class UserAccount
    {
        public string username { get; private set; }
        public string password { get; private set; }
        // Should be a more specialized type for dates
        public DateTime dateCreated { get; private set; }
        public Permission permissionLevel { get; private set; }

        public enum Permission
        {
            user = 0,
            employee = 1,
            admin = 2
        }

        public UserAccount(string username, string password, DateTime dateCreated, Permission permissionLevel)
        {
            this.username = username;
            this.password = password;
            this.dateCreated = dateCreated;
            this.permissionLevel = permissionLevel;
        }

        public bool CheckPassword(string attempt)
        {
            return string.Equals(password, attempt);
        }

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
