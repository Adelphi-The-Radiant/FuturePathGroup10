using System;

namespace FuturePathGroup10
{
	public abstract class AccountHolder(string username)
    {
        public string Username { get; init; } = username;
        public UserAccount? Account { get; set; }
    }
}
