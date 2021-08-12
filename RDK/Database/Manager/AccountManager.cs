using RDK.Database.Models;
using RDK.Database.Core;
using System.Linq;

namespace RDK.Database.Manager
{
    public class AccountManager : DatabaseRequest<Account, DatabaseContext>
    {
        public AccountManager(DatabaseContext context) : base(context) { }

        public Account GetAccount(string username, string passwordHash)
            => Context.Accounts.FirstOrDefault(a => a.Username == username && a.PasswordHash == passwordHash);

        public Account GetAccount(long accountId)
            => Context.Accounts.FirstOrDefault(a => a.Id == accountId);

        public bool AccountExists(string username)
            => Context.Accounts.Any(a => a.Username == username);

        public bool Authenticate(string username, string password, out Account account)
        {
            Account dbAccount = Context.Accounts.SingleOrDefault(x => x.Username == username);
            if (BCrypt.Net.BCrypt.Verify(password, dbAccount.PasswordHash))
            {
                account = dbAccount;
                return true;
            }

            account = null;
            return false;
        }
    }
}
