using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Services;
using SimpleBankingSystem.Domain.Tests.Stubs;

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
        /// The service for business rules
        /// </summary>
        protected IBusinessRulesService RulesService;

        /// <summary>
        /// The user
        /// </summary>
        protected User User;

        [TestInitialize]
        public virtual void Init()
        {
            var accountRepository = new AccountRepositoryStub();
            var transactionRepository = new TransactionRepositoryStub();

            RulesService = new BusinessRulesService();
            BankService = new BankService(accountRepository, transactionRepository, RulesService);
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
