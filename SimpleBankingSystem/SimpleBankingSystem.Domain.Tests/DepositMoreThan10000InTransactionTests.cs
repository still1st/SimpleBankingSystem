using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.BusinessRules;
using SimpleBankingSystem.Domain.Models;
using System.Linq;

namespace SimpleBankingSystem.Domain.Tests
{
    [TestClass]
    public class DepositMoreThan10000InTransactionTests : BusinessRulesTestBase
    {
        [TestMethod]
        public void DepositMoreThan10000InTransaction_Deposit9000_ShouldNotReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 9000m);

            // ACT
            var rule = new UserCantDepositMoreThan10000InTransaction(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(true, canExecute);
            Assert.AreEqual(0, errors.Count());
        }

        [TestMethod]
        public void DepositMoreThan10000InTransaction_Deposit10500_ShouldReturnError()
        {
            // ARRANGE
            var transaction = BankService.CreateTransaction(TransactionType.Deposit, Account, 10500m);

            // ACT
            var rule = new UserCantDepositMoreThan10000InTransaction(transaction);
            var canExecute = rule.CanExecuteTransaction();
            var errors = rule.GetErrors();

            // ASSERT
            Assert.AreEqual(false, canExecute);
            Assert.AreEqual(1, errors.Count());
        }
    }
}
