using System;
using System.Collections.Generic;

namespace FuturePathGroup10
{
    public class Utilities
    {
        //Finds an account holder with the same username as a certain account and assigns that account to that account holder.
        public static bool FindAndSetAccountHolder(IEnumerable<AccountHolder> accountHolders, UserAccount account)
        {
            foreach (AccountHolder holder in accountHolders)
            {
                if (holder.username.Equals(account.username))
                {
                    holder.setAccount(account);
                    return true;
                }
            }
            return false;
        }
    }
}
