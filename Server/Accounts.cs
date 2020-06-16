using System.Collections.Concurrent;
using Common;

namespace Server
{
    public static class Accounts
    {
        public static ConcurrentDictionary<string, Account> AccountsByName { get; set; } = new ConcurrentDictionary<string, Account>();

        public static Account RequireAccount(string accountName)
        {
            if (AccountsByName.TryGetValue(accountName, out var account))
            {
                return account;
            }
            account = new Account(accountName);
            AccountsByName.TryAdd(accountName, account);
            return account;
        }
    }
}