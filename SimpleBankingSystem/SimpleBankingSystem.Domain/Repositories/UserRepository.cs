using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Repositories
{
    /// <summary>
    /// Default implementation for <see cref="IUserRepository"/>
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
    }
}
