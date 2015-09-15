using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.Domain.Services.TransactionExecutors
{
    /// <summary>
    /// The executor is to deposit an account
    /// </summary>
    class DepositTransactionExecutor : ITransactionExecutor
    {
        /// <summary>
        /// Executes the transaction
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        public void Execute(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            if (transaction.IsCommited)
                throw new ArgumentException("Cann't execute a commited transaction");

            var account = transaction.Account;
            var amount = transaction.Amount;
            account.Balance += amount;

            transaction.IsCommited = true;
        }
    }
}
