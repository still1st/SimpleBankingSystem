using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;

namespace SimpleBankingSystem.Domain.Tests.Stubs
{
    /// <summary>
    /// Stub implementation for <see cref="IUserRepository"/>
    /// </summary>
    public class UserRepositoryStub : RepositoryStub<User>, IUserRepository
    {
    }
}
