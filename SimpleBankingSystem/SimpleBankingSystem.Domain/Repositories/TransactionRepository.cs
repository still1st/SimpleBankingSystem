using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Repositories
{
    /// <summary>
    /// Default implementation for <see cref="ITransactionRepository"/>
    /// </summary>
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
    }
}
