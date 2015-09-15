using SimpleBankingSystem.Domain.Models;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Repositories
{
    /// <summary>
    /// Default implementation for <see cref="IAccountRepository"/>
    /// </summary>
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        /// <summary>
        /// Gets accounts for the user
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Collection of the <see cref="Account"/> entities</returns>
        public IEnumerable<Account> GetAccountsForUser(User user)
        {
            return Entities.Where(x => x.User == user);
        }
    }
}
