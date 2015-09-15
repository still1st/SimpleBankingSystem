using SimpleBankingSystem.Domain.BusinessRules;
using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Service for banking logic
    /// </summary>
    public interface IBankService
    {
        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Created <see cref="Account"/> entity</returns>
        Account CreateAccount(User user);

        /// <summary>
        /// Gets all accounts for the user
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Collection of the <see cref="Account"/> entities for the user</returns>
        IEnumerable<Account> GetAccountsForUser(User user);

        /// <summary>
        /// Removes an account
        /// </summary>
        /// <param name="account"><see cref="Account"/> entity</param>
        void RemoveAccount(Account account);

        /// <summary>
        /// Creates a new transaction
        /// </summary>
        /// <param name="transactionType">The type of the transaction</param>
        /// <param name="account"><see cref="Account"/> entity</param>
        /// <param name="amount">Amount</param>
        /// <returns>Created transaction</returns>
        Transaction CreateTransaction(TransactionType transactionType, Account account, Decimal amount);

        /// <summary>
        /// Executes the transaction
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        IEnumerable<BusinessError> ExecuteTransaction(SimpleBankingSystem.Domain.Models.Transaction transaction);
    }
}
