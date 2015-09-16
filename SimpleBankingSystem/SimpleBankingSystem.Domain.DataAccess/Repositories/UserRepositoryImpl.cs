using SimpleBankingSystem.Domain.DataAccess.Base;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;

namespace SimpleBankingSystem.Domain.DataAccess.Repositories
{
    /// <summary>
    /// Default implementation of <see cref="IUserRepository"/>
    /// </summary>
    public class UserRepositoryImpl : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepositoryImpl"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public UserRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {
        }
    }
}
