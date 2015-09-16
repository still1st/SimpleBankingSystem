using SimpleBankingSystem.Domain.DataAccess.Base;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;

namespace SimpleBankingSystem.Domain.DataAccess.Repositories
{
    /// <summary>
    /// Default implementation of <see cref="TransactionRepositoryImpl"/>
    /// </summary>
    public class TransactionRepositoryImpl : RepositoryBase<Transaction>, ITransactionRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TransactionRepositoryImpl"/> class.
        /// </summary>
        /// <param name="contextFactory">The context factory.</param>
        public TransactionRepositoryImpl(IContextFactory contextFactory)
            : base(contextFactory)
        {
        }
    }
}
