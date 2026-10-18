using System;
using System.Collections.Generic;

namespace FuturePathGroup10
{
    public class Utilities
    {
        //Finds an Account holder with the same Username as a certain Account and assigns that Account to that Account holder.
        public static bool FindAndSetAccountHolder(IEnumerable<AccountHolder> accountHolders, UserAccount account)
        {
            foreach (AccountHolder holder in accountHolders)
            {
                if (holder.Username.Equals(account.username))
                {
                    holder.Account = account;
                    return true;
                }
            }
            return false;
        }
    }
}
