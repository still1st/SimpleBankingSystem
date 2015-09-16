using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Tests.Stubs
{
    /// <summary>
    /// Stub implementation for <see cref="IAccountRepository"/>
    /// </summary>
    public class AccountRepositoryStub : RepositoryStub<Account>, IAccountRepository
    {
        /// <summary>
        /// Gets accounts for the user
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Collection of the <see cref="Account"/> entities</returns>
        public IQueryable<Account> GetAccountsForUser(User user)
        {
            return Entities.AsQueryable().Where(x => x.User == user);
        }
    }
}
