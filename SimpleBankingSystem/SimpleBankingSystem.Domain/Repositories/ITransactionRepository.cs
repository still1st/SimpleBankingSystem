using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Repositories
{
    /// <summary>
    /// Repository for <see cref="Transaction"/> entities
    /// </summary>
    public interface ITransactionRepository : IRepository<Transaction>
    {
    }
}
