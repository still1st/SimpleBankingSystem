using SimpleBankingSystem.Domain.BusinessRules;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Services.TransactionExecutors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Default implementation of <see cref="IBankService"/>
    /// </summary>
    public class BankService : IBankService
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
        /// Dictionary of <see cref="ITransactionExecutors"/> by transaction types
        /// </summary>
        private Dictionary<TransactionType, ITransactionExecutor> _transactionExecutors;

        /// <summary>
        /// Service for business rules
        /// </summary>
        private IBusinessRulesService _rulesService;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="BankService"/>
        /// </summary>
        public BankService(IAccountRepository accountRepository,
            ITransactionRepository transactionRepository,
            IBusinessRulesService rulesService)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _rulesService = rulesService;

            InitTransactionExecutors();
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
        /// Gets account by id
        /// </summary>
        /// <param name="accountId">Account Id</param>
        /// <returns><see cref="Account"/> entity</returns>
        public Account GetAccountById(Int64 accountId)
        {
            return _accountRepository.GetById(accountId);
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

            // first delete all transactions
            var transactions = _transactionRepository.Query().Where(x => x.Account.AccountId == account.AccountId);
            _transactionRepository.DeleteRange(transactions);

            // delete account
            _accountRepository.Delete(account);
        }

        /// <summary>
        /// Gets transaction by account
        /// </summary>
        /// <param name="account"><see cref="Account"/> entity</param>
        /// <returns>Collection of <see cref="Transaction"/> entities</returns>
        public IEnumerable<Transaction> GetTransactionsByAccount(Account account)
        {
            if (account == null)
                throw new ArgumentNullException("account");

            return _transactionRepository.Query().Where(x => x.Account.AccountId == account.AccountId);
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
        public IEnumerable<BusinessError> ExecuteTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            if (transaction.IsCommited)
                throw new ArgumentException("Cann't execute a commited transaction");

            if (!DoesTheTransactionExist(transaction))
                throw new ArgumentException(String.Format("The transaction with ID {0} wasn't found", transaction.TransactionId));

            // validate transaction by business rules
            var userAccounts = _accountRepository.GetAccountsForUser(transaction.Account.User);
            var errors = _rulesService.Validate(userAccounts, transaction);
            var isValid = !errors.Any();

            // if it doesn't have errors then execute transaction
            if (isValid)
            {
                var executer = GetTransactionExecutorByType(transaction.Type);
                executer.Execute(transaction);
            }

            _transactionRepository.Update(transaction);

            return errors;
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

            return _transactionRepository.Query().Any(x => x.TransactionId == transaction.TransactionId);
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

            return _accountRepository.Query().Any(x => x.AccountId == account.AccountId);
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

        /// <summary>
        /// Gets a transaction executor by transaction type
        /// </summary>
        /// <param name="transactionType">The transaction type</param>
        /// <returns>Transaction executer</returns>
        private ITransactionExecutor GetTransactionExecutorByType(TransactionType transactionType)
        {
            if (_transactionExecutors.ContainsKey(transactionType))
                return _transactionExecutors[transactionType];

            throw new ArgumentException(String.Format("Transaction executor for type {0} wasn't found", transactionType));
        }
        #endregion
    }
}
