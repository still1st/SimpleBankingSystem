using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Service for banking logic
    /// </summary>
    public class BankService
    {
        #region private fields
        /// <summary>
        /// The repository for <see cref="Account"/> entities
        /// </summary>
        private IAccountRepository _accountRepository;

        /// <summary>
        /// The repository for <see cref="Transaction"/> entities
        /// </summary>
        private ITransactionRepository _transactionRepository;

        /// <summary>
        /// The repository for <see cref="User"/> entities
        /// </summary>
        private IUserRepository _userRepository;

        /// <summary>
        /// The transaction service
        /// </summary>
        private TransactionService _transactionService; 
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="BankService"/>
        /// </summary>
        public BankService(IAccountRepository accountRepository,
            IUserRepository userRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _userRepository = userRepository;
            _transactionRepository = transactionRepository;

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

            var account = new Account
            {
                Balance = 0m,
                User = user
            };

            _accountRepository.Add(account);

            return account;
        }

        /// <summary>
        /// Gets all accounts for the user
        /// </summary>
        /// <param name="user"><see cref="User"/> entity</param>
        /// <returns>Collection of the <see cref="Account"/> entities for the user</returns>
        public IEnumerable<Account> GetAccountsForUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            var accounts = _accountRepository.GetAccountsForUser(user);
            return accounts;
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

            _accountRepository.Delete(account);
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

            _transactionRepository.Add(transaction);

            return transaction;
        }

        /// <summary>
        /// Executes the transaction
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
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

            return _transactionRepository.GetAll().Contains(transaction);
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

            return _accountRepository.GetAll().Any(x => x.User == user);
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

            var accounts = _accountRepository.GetAll().Where(x => x.User == user);
            return accounts.Contains(account);
        } 
        #endregion
    }
}
