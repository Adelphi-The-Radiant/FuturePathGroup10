using System;
using FuturePathGroup10;

namespace FuturePathGroup10.Data
{
	public class UserAccountDB : DB
	{
		public const USERACCOUNT_TABLE = "UserAccount";
		public const SQL_SELECT_USERACCOUNT = "SELECT * FROM UserAccount";
		public Collection<UserAccount> accounts { get; private set; }
		public UserAccountDB()
		{
			accounts = new Collection<UserAccount>;
			FillDataSet(SQL_SELECT_USERACCOUNT,USERACCOUNT_TABLE)

		}
	}
}
