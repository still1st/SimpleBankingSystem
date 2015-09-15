using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Tests
{
    [TestClass]
    public class BankServiceTests
    {
        /// <summary>
        /// The service is to execute banking logic
        /// </summary>
        private BankService _bankService;

        /// <summary>
        /// The user
        /// </summary>
        private User _user;

        [TestInitialize]
        public void Init()
        {
            _bankService = new BankService();
            _user = CreateUser();
        }

        [TestMethod]
        public void CreateAccount_ShouldReturnCreatedAccount()
        {
            // ARRANGE

            // ACT
            var account = _bankService.CreateAccount(_user);

            // ASSERT
            Assert.IsNotNull(account);
            Assert.AreEqual(0m, account.Balance);
            Assert.AreEqual(_user, account.User);
        }

        [TestMethod]
        public void GetUserAccounts_ShouldReturnAllAccountsForTheUser()
        {
            // ARRANGE
            var account1 = _bankService.CreateAccount(_user);
            var account2 = _bankService.CreateAccount(_user);

            // ACT
            var accounts = _bankService.GetAccountsForUser(_user);

            // ASSERT
            Assert.IsNotNull(accounts);
            CollectionAssert.AreEqual(new List<Account> { account1, account2 }, accounts.ToList());
        }

        [TestMethod]
        public void DeleteAccount_AccountShouldBeRemoved()
        {
            // ARRANGE
            var account = _bankService.CreateAccount(_user);

            // ACT
            _bankService.RemoveAccount(account);

            // ASSERT
            var accounts = _bankService.GetAccountsForUser(_user);
            Assert.IsTrue(accounts == null || !accounts.Any());
        }

        [TestMethod]
        public void CreateDepositTransaction_ShouldReturnDepositTransaction()
        {
            // ARRANGE
            var account = _bankService.CreateAccount(_user);
            var amount = 1000m;
            var transactionType = TransactionType.Deposit;

            // ACT
            var transaction = _bankService.CreateTransaction(transactionType, account, amount);

            // ASSERT
            Assert.IsNotNull(transaction);
            Assert.AreEqual(amount, transaction.Amount);
            Assert.AreEqual(transactionType, transaction.Type);
            Assert.AreEqual(account, transaction.Account);
            Assert.AreEqual(false, transaction.IsCommited);
        }

        [TestMethod]
        public void CreateWithdrawTransaction_ShouldReturnWithdrawTransaction()
        {
            // ARRANGE
            var account = _bankService.CreateAccount(_user);
            var amount = 500m;
            var transactionType = TransactionType.Withdraw;

            // ACT
            var transaction = _bankService.CreateTransaction(transactionType, account, amount);

            // ASSERT
            Assert.IsNotNull(transaction);
            Assert.AreEqual(amount, transaction.Amount);
            Assert.AreEqual(transactionType, transaction.Type);
            Assert.AreEqual(account, transaction.Account);
            Assert.AreEqual(false, transaction.IsCommited);
        }

        [TestMethod]
        public void ExecuteDepositTransaction_AccountShouldBeIncreased()
        {
            // ARRANGE
            var account = _bankService.CreateAccount(_user);
            var amount = 500m;
            var transactionType = TransactionType.Deposit;
            var transaction = _bankService.CreateTransaction(transactionType, account, amount);

            // ACT
            _bankService.ExecuteTransaction(transaction);

            // ASSERT
            Assert.AreEqual(amount, account.Balance);
            Assert.AreEqual(true, transaction.IsCommited);
        }

        [TestMethod]
        public void ExecuteWithdrawTransaction_AccountShouldBeReduced()
        {
            // ARRANGE
            var account = _bankService.CreateAccount(_user);
            var balance = account.Balance;
            var amount = 1000m;
            var transactionType = TransactionType.Withdraw;
            var transaction = _bankService.CreateTransaction(transactionType, account, amount);

            // ACT
            _bankService.ExecuteTransaction(transaction);

            // ASSERT
            Assert.AreEqual(balance - amount, account.Balance);
            Assert.AreEqual(true, transaction.IsCommited);
        }

        [Ignore]
        private User CreateUser()
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Johnson"
            };

            return user;
        }
    }
}
