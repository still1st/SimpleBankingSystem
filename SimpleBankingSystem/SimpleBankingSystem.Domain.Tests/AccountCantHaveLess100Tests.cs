using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.BusinessRules;
using SimpleBankingSystem.Domain.Models;
using System.Linq;

namespace SimpleBankingSystem.Domain.Tests
{
    [TestClass]
    public class AccountCantHaveLess100Tests : BusinessRulesTestBase
    {
        [TestMethod]
        public void AccountCantHaveLess100_ItWas0ThenDeposit50_ShouldReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 50m);

            // ACT
            var rule = new AccountCantHaveLess100(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(false, canExecute);
            Assert.AreEqual(1, errors.Count());
        }

        [TestMethod]
        public void AccountCantHaveLess100_ItWas0ThenDeposit150_ShouldNotReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 150m);

            // ACT
            var rule = new AccountCantHaveLess100(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(true, canExecute);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void AccountCantHaveLess100_ItWas150ThenDeposit10_ShouldNoReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 150m);
            BankService.ExecuteTransaction(transaction);
            var transaction2 = BankService.CreateTransaction(TransactionType.Deposit, Account, 10m);

            // ACT
            var rule = new AccountCantHaveLess100(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(true, canExecute);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void AccountCantHaveLess100_ItWas0ThenWithdraw100_ShouldReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Withdraw, Account, 100m);

            // ACT
            var rule = new AccountCantHaveLess100(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(false, canExecute);
            Assert.AreEqual(1, errors.Count());
        }

        [TestMethod]
        public void AccountCantHaveLess100_ItWas150ThenWithdraw10_ShouldNotReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 150m);
            BankService.ExecuteTransaction(transaction);
            var transaction2 = BankService.CreateTransaction(TransactionType.Withdraw, Account, 10m);

            // ACT
            var rule = new AccountCantHaveLess100(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(true, canExecute);
            Assert.AreEqual(0, errors.Count());
        }
    }
}
