using SimpleBankingSystem.Domain.Models;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Repositories
{
    /// <summary>
    /// Repository for <see cref="Account"/> entities
    /// </summary>
    public interface IAccountRepository : IRepository<Account>
    {
        /// <summary>
        /// Gets accounts for the user
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Collection of the <see cref="Account"/> entities</returns>
        IEnumerable<Account> GetAccountsForUser(User user);
    }
}
