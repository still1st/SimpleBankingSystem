using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Services.TransactionExecuters
{
    /// <summary>
    /// Interface for transaction executer
    /// </summary>
    interface ITransactionExecutor
    {
        /// <summary>
        /// Executes the transaction
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        void Execute(Transaction transaction);
    }
}
