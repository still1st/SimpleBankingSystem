using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Services;

namespace SimpleBankingSystem.Domain.Tests
{
    /// <summary>
    /// Base test class
    /// </summary>
    [TestClass]
    public abstract class TestBase
    {
        /// <summary>
        /// The service for banking logic
        /// </summary>
        protected IBankService BankService;

        /// <summary>
        /// The user
        /// </summary>
        protected User User;

        [TestInitialize]
        public virtual void Init()
        {
            var accountRepository = new AccountRepository();
            var transactionRepository = new TransactionRepository();

            BankService = new BankService(accountRepository, transactionRepository);
            User = CreateUser();
        }

        [Ignore]
        protected User CreateUser()
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
