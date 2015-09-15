using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.BusinessRules;
using System.Linq;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Tests
{
    [TestClass]
    public class WithdrawMoreThan90PercentTotalBalanceTests : BusinessRulesTestBase
    {
        /// <summary>
        /// The second account of the user
        /// </summary>
        private Account _account2;

        [TestInitialize]
        public override void Init()
        {
            base.Init();
            _account2 = BankService.CreateAccount(User);
        }

        [TestMethod]
        public void WithdrawMoreThan90PercentTotalBalance_Total10000Withdraw9500_ShouldReturnError()
        {
            // ARRANGE
            var depositTransaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 9700m);
            var depositTransaction2 = BankService.CreateTransaction(TransactionType.Deposit, _account2, 300m);
            BankService.ExecuteTransaction(depositTransaction);
            BankService.ExecuteTransaction(depositTransaction2);
            var accounts = new List<Account> { Account, _account2 };

            // ACT
            var transaction = BankService.CreateTransaction(TransactionType.Withdraw, Account, 9500m);
            var rule = new UserCantWithdrawMoreThan90PercentTotalBalance(accounts, transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(false, canExecute);
            Assert.AreEqual(1, errors.Count());
        }

        [TestMethod]
        public void WithdrawMoreThan90PercentTotalBalance_Total10000Withdraw5000_ShouldNotReturnError()
        {
            // ARRANGE
            var depositTransaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 7000m);
            var depositTransaction2 = BankService.CreateTransaction(TransactionType.Deposit, _account2, 3000m);
            BankService.ExecuteTransaction(depositTransaction);
            BankService.ExecuteTransaction(depositTransaction2);
            var accounts = new List<Account> { Account, _account2 };

            // ACT
            var transaction = BankService.CreateTransaction(TransactionType.Withdraw, Account, 5000m);
            var rule = new UserCantWithdrawMoreThan90PercentTotalBalance(accounts, transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(true, canExecute);
            Assert.AreEqual(0, errors.Count());
        }
    }
}
