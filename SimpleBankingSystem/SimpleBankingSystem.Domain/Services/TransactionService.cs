using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Services.TransactionExecuters;
using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Service for transaction executing
    /// </summary>
    public class TransactionService
    {
        /// <summary>
        /// Dictionary of <see cref="ITransactionExecutors"/> by transaction types
        /// </summary>
        private Dictionary<TransactionType, ITransactionExecutor> _transactionExecutors;

        /// <summary>
        /// Initializes a new instance of <see cref="TransactionService"/>
        /// </summary>
        public TransactionService()
        {
            InitTransactionExecutors();
        }

        /// <summary>
        /// Executes the transaction
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        public void Execute(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            var executer = GetTransactionExecutorByType(transaction.Type);
            executer.Execute(transaction);
        }

        /// <summary>
        /// Gets a transaction executor by transaction type
        /// </summary>
        /// <param name="transactionType">The transaction type</param>
        /// <returns>Transaction executer</returns>
        private ITransactionExecutor GetTransactionExecutorByType(TransactionType transactionType)
        {
            if (_transactionExecutors.ContainsKey(transactionType))
                return _transactionExecutors[transactionType];

            throw new KeyNotFoundException(String.Format("Transaction executer for type {0} wasn't found", transactionType));
        }

        /// <summary>
        /// Initializes a dictionary of <see cref="ITransactionExecutor"/>
        /// </summary>
        private void InitTransactionExecutors()
        {
            _transactionExecutors = new Dictionary<TransactionType, ITransactionExecutor> 
            {
                {TransactionType.Deposit, new DepositTransactionExecutor()},
                {TransactionType.Withdraw, new WithdrawTransactionExecutor()}
            };
        }
    }
}
