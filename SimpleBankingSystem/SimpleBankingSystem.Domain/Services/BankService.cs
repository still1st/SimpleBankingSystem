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
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a new instance of <see cref="BankService"/>
        /// </summary>
        public BankService(IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;

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
        public IEnumerable<BusinessError> ExecuteTransaction(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            if (transaction.IsCommited)
                throw new ArgumentException("Cann't execute a commited transaction");

            if (!DoesTheTransactionExist(transaction))
                throw new ArgumentException(String.Format("The transaction with ID {0} wasn't found", transaction.TransactionId));

            // validate transaction by business rules
            var rules = GetBusinessRules(transaction);
            var errors = new List<BusinessError>();
            foreach (var rule in rules)
            {
                rule.CanExecuteTransaction();
                errors.AddRange(rule.GetErrors());
            }

            // if it doesn't have errors then execute transaction
            if (!errors.Any())
            {
                var executer = GetTransactionExecutorByType(transaction.Type);
                executer.Execute(transaction);
            }

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

            return _transactionRepository.GetAll().Contains(transaction);
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
            var accounts = _accountRepository.GetAll().Where(x => x.User == user);

            return accounts.Contains(account);
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

            throw new KeyNotFoundException(String.Format("Transaction executer for type {0} wasn't found", transactionType));
        }

        /// <summary>
        /// Gets business rules
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        /// <returns></returns>
        private IEnumerable<IBusinessRule> GetBusinessRules(Transaction transaction)
        {
            var accounts = _accountRepository.GetAccountsForUser(transaction.Account.User);

            var rule1 = new AccountCantHaveLess100(transaction);
            var rule2 = new UserCantWithdrawMoreThan90PercentTotalBalance(accounts, transaction);
            var rule3 = new UserCantDepositMoreThan10000InTransaction(transaction);

            return new List<IBusinessRule> { rule1, rule2, rule3};
        }
        #endregion
    }
}
