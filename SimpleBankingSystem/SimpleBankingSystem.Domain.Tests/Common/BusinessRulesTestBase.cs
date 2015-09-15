using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBankingSystem.Domain.Models;

namespace SimpleBankingSystem.Domain.Tests
{
    [TestClass]
    public abstract class BusinessRulesTestBase : TestBase
    {
        /// <summary>
        /// <see cref="Account"/> entity
        /// </summary>
        protected Account Account;

        [TestInitialize]
        public override void Init()
        {
            base.Init();
            Account = BankService.CreateAccount(User);
        }
    }
}
