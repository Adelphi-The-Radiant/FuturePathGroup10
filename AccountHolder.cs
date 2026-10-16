using System;

namespace FuturePathGroup10
{

	public class AccountHolder
	{
		public string username { get; private set; }
		public UserAccount account { get; private set; }
		public AccountHolder(string username)
		{
			this.username = username;
		}

		public void setAccount(UserAccount account)
		{
			this.account = account;
		}

        public UserAccount getAccount(string username)
        {
            if (this.username.Equals(username))
            {
                return account;
            }
            else
            {
                throw new Exception("Username does not match.");
            }
        }
    }
}
