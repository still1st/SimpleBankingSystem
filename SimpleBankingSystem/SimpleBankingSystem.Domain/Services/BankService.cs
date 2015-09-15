using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Service for banking logic
    /// </summary>
    public class BankService
    {
        #region private fields
        /// <summary>
        /// The accounts of the users
        /// </summary>
        private Dictionary<User, List<Account>> _accountsByUsers;

        /// <summary>
        /// The collection of the transactions
        /// </summary>
        private List<Transaction> _transactions;

        /// <summary>
        /// The transaction service
        /// </summary>
        private TransactionService _transactionService; 
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="BankService"/>
        /// </summary>
        public BankService()
        {
            _accountsByUsers = new Dictionary<User, List<Account>>();
            _transactions = new List<Transaction>();
            _transactionService = new TransactionService();
        } 
        #endregion

        /// <summary>
        /// Creates a new account
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Created <see cref="Account"/> entity</returns>
        public Account CreateAccount(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            // try to get exist accounts
            List<Account> accounts;
            if (!_accountsByUsers.TryGetValue(user, out accounts))
            {
                accounts = new List<Account>();
                _accountsByUsers[user] = accounts;
            }
                
            var account = new Account
            {
                Balance = 0m,
                User = user
            };

            accounts.Add(account);

            return account;
        }

        /// <summary>
        /// Gets all accounts for the user
        /// Returns null if user doesn't have accounts
        /// else returns all the user's accounts
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Collection of the user's <see cref="Account"/> entities</returns>
        public IEnumerable<Account> GetAccountsForUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            if (_accountsByUsers.ContainsKey(user))
                return _accountsByUsers[user];

            return null;
        }

        /// <summary>
        /// Removes an account
        /// </summary>
        /// <param name="account"><see cref="Account"/> entity</param>
        public void RemoveAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            if (!DoesTheAccountExist(account))
                throw new ArgumentException(String.Format("Account with ID {0} wasn't found", account.AccountId));

            var accounts = _accountsByUsers[account.User];
            accounts.Remove(account);
        }

        /// <summary>
        /// Creates a new transaction
        /// </summary>
        /// <param name="transactionType">The type of the transaction</param>
        /// <param name="account"><see cref="Account"/> entity</param>
        /// <param name="amount">Amount</param>
        /// <returns>Created transaction</returns>
        public Transaction CreateTransaction(TransactionType transactionType, Account account, Decimal amount)
        {
            if (transactionType == TransactionType.NotSpecified)
                throw new ArgumentException("The transaction's type cann't be not specified");

            if (account == null)
                throw new ArgumentNullException("account");

            if (amount <= 0)
                throw new ArgumentException("Amount cann't be less or equal 0");

            if (!DoesTheAccountExist(account))
                throw new ArgumentException(String.Format("Account with ID {0} wasn't found", account.AccountId));

            var transaction = new Transaction
            {
                Account = account,
                Amount = amount,
                Type = transactionType,
                IsCommited = false
            };

            _transactions.Add(transaction);

            return transaction;
        }

        public void ExecuteTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            if (transaction.IsCommited)
                throw new ArgumentException("Cann't execute a commited transaction");

            if (!DoesTheTransactionExist(transaction))
                throw new ArgumentException(String.Format("The transaction with ID {0} wasn't found", transaction.TransactionId));

            _transactionService.Execute(transaction);
        }

        #region private methods
        /// <summary>
        /// Checks exist the transaction in the collection of the transactions 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private Boolean DoesTheTransactionExist(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            return _transactions.Contains(transaction);
        }

        /// <summary>
        /// Checks exist the user in the collections of the accounts
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>TRUE - if the user exists in the accounts base, FALSE - the user doesn't exist there</returns>
        private Boolean DoesTheUserExistInAccounts(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            return _accountsByUsers.ContainsKey(user);
        }

        /// <summary>
        /// Checks exist the account in the collection of the accounts
        /// </summary>
        /// <param name="account"><see cref="Account"/> entity</param>
        /// <returns>TRUE - if the account exists, FALSE - if the account doesn't exist</returns>
        private Boolean DoesTheAccountExist(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            var user = account.User;

            if (!DoesTheUserExistInAccounts(account.User))
                throw new ArgumentException(String.Format("Owner of an account with ID {0} wasn't found", account.AccountId));

            var accounts = _accountsByUsers[user];

            return accounts.Contains(account);
        } 
        #endregion
    }
}
