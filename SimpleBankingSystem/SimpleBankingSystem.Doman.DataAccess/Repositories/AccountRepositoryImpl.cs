using SimpleBankingSystem.Domain.DataAccess.Base;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.DataAccess.Repositories
{
    /// <summary>
    /// Default implementation of <see cref="AccountRepositoryImpl"/>
    /// </summary>
    public class AccountRepositoryImpl : RepositoryBase<Account>, IAccountRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountRepositoryImpl"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public AccountRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {
        }

        public IQueryable<Account> GetAccountsForUser(User user)
        {
            return Query().Where(x => x.User.UserId == user.UserId);
        }
    }
}
