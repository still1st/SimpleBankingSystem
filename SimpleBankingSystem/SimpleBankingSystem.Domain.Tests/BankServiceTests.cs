using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Services;
using System.Collections.Generic;
using System.Linq;

namespace SimpleBankingSystem.Domain.Tests
{
    [TestClass]
    public class BankServiceTests : TestBase
    {
        [TestMethod]
        public void CreateAccount_ShouldReturnCreatedAccount()
        {
            // ARRANGE

            // ACT
            var account = BankService.CreateAccount(User);

            // ASSERT
            Assert.IsNotNull(account);
            Assert.AreEqual(0m, account.Balance);
            Assert.AreEqual(User, account.User);
        }

        [TestMethod]
        public void GetUserAccounts_ShouldReturnAllAccountsForTheUser()
        {
            // ARRANGE
            var account1 = BankService.CreateAccount(User);
            var account2 = BankService.CreateAccount(User);

            // ACT
            var accounts = BankService.GetAccountsForUser(User);

            // ASSERT
            Assert.IsNotNull(accounts);
            CollectionAssert.AreEqual(new List<Account> { account1, account2 }, accounts.ToList());
        }

        [TestMethod]
        public void RemoveAccount_AccountShouldBeRemoved()
        {
            // ARRANGE
            var account = BankService.CreateAccount(User);

            // ACT
            BankService.RemoveAccount(account);

            // ASSERT
            var accounts = BankService.GetAccountsForUser(User);
            Assert.IsTrue(accounts == null || !accounts.Any());
        }

        [TestMethod]
        public void CreateDepositTransaction_ShouldReturnDepositTransaction()
        {
            // ARRANGE
            var account = BankService.CreateAccount(User);
            var amount = 1000m;
            var transactionType = TransactionType.Deposit;

            // ACT
            var transaction = BankService.CreateTransaction(transactionType, account, amount);

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
            var account = BankService.CreateAccount(User);
            var amount = 500m;
            var transactionType = TransactionType.Withdraw;

            // ACT
            var transaction = BankService.CreateTransaction(transactionType, account, amount);

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
            var account = BankService.CreateAccount(User);
            var amount = 500m;
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, account, amount);

            // ACT
            BankService.ExecuteTransaction(transaction);

            // ASSERT
            Assert.AreEqual(amount, account.Balance);
            Assert.AreEqual(true, transaction.IsCommited);
        }

        [TestMethod]
        public void ExecuteWithdrawTransaction_AccountShouldBeReduced()
        {
            // ARRANGE
            var account = BankService.CreateAccount(User);
            var depositTransaction = BankService.CreateTransaction(TransactionType.Deposit, account, 1000m);
            var withdrawTransaction = BankService.CreateTransaction(TransactionType.Withdraw, account, 300m);
            var balance = 700m;

            // ACT
            BankService.ExecuteTransaction(depositTransaction);
            BankService.ExecuteTransaction(withdrawTransaction);

            // ASSERT
            Assert.AreEqual(balance, account.Balance);
            Assert.AreEqual(true, withdrawTransaction.IsCommited);
        }
    }
}
