using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.Domain.Services.TransactionExecuters
{
    class WithdrawTransactionExecutor : ITransactionExecutor
    {
        public void Execute(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            if (transaction.IsCommited)
                throw new ArgumentException("Cann't execute a commited transaction");

            var account = transaction.Account;
            var amount = transaction.Amount;
            account.Balance -= amount;

            transaction.IsCommited = true;
        }
    }
}
