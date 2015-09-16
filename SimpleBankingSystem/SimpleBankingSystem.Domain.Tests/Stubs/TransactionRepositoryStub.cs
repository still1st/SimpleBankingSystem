using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;

namespace SimpleBankingSystem.Domain.Tests.Stubs
{
    /// <summary>
    /// Stub implementation for <see cref="ITransactionRepository"/>
    /// </summary>
    public class TransactionRepositoryStub : RepositoryStub<Transaction>, ITransactionRepository
    {
    }
}
