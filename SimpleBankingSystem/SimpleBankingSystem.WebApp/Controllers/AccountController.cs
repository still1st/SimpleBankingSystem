using SimpleBankingSystem.Domain.DataAccess.Base;
using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Domain.Repositories;
using SimpleBankingSystem.Domain.Services;
using SimpleBankingSystem.WebApp.ViewModels;
using System;
using System.Linq;
using System.Web.Http;

namespace SimpleBankingSystem.WebApp.Controllers
{
    [RoutePrefix("api/account")]
    public class AccountController : BaseApiController
    {
        private IBankService _bankService;
        private IUnitOfWork _uow;
        private IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="AccountController"/>
        /// </summary>
        /// <param name="bankService">Service for banking logic</param>
        /// <param name="userRepository">Repository for <see cref="User"/> entity</param>
        /// <param name="uow">Unity of work</param>
        public AccountController(IBankService bankService, 
            IUserRepository userRepository, 
            IUnitOfWork uow)
        {
            _bankService = bankService;
            _userRepository = userRepository;
            _uow = uow;
        }

        /// <summary>
        /// Gets all accounts for the user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("List")]
        public IHttpActionResult Accounts()
        {
            var user = _userRepository.Query().First();
            var accounts = _bankService.GetAccountsForUser(user);

            return OkResult(accounts);
        }

        /// <summary>
        /// Gets details for the account
        /// </summary>
        /// <param name="accountId">ID of the account</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Details")]
        public IHttpActionResult Details(Int64 accountId)
        {
            var account = _bankService.GetAccountById(accountId);
            if (account == null)
                return NotFound();

            return OkResult(account);
        }

        /// <summary>
        /// Adds account
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Add")]
        public IHttpActionResult AddAccount()
        {
            var user = _userRepository.Query().First();
            var account = _bankService.CreateAccount(user);
            _uow.Commit();

            return OkResult(account);
        }

        /// <summary>
        /// Removes the account
        /// </summary>
        /// <param name="accountId">ID of the account</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Remove")]
        public IHttpActionResult RemoveAccount(Int64 accountId)
        {
            var account = _bankService.GetAccountById(accountId);
            if (account == null)
                return NotFound();

            _bankService.RemoveAccount(account);
            _uow.Commit();

            return OkResult();
        }

        /// <summary>
        /// Executes transaction
        /// </summary>
        /// <param name="accountId">ID of the account</param>
        /// <param name="type">Transaction type</param>
        /// <param name="amount">Amount</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Transaction")]
        public IHttpActionResult ExecuteTransaction(Int64 accountId, TransactionType type, Decimal amount)
        {
            var account = _bankService.GetAccountById(accountId);
            if (account == null)
                return NotFound();

            var transaction = _bankService.CreateTransaction(type, account, amount);
            _uow.Commit();
            _bankService.ExecuteTransaction(transaction);
            _uow.Commit();

            var vm = new
            {
                Balance = account.Balance,
                Transaction = new TransactionViewModel(transaction)
            };

            return OkResult(vm);
        }

        /// <summary>
        /// Gets all transaction fot the accpount
        /// </summary>
        /// <param name="accountId">ID of the account</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Transactions")]
        public IHttpActionResult Transactions(Int64 accountId)
        {
            var account = _bankService.GetAccountById(accountId);
            if (account == null)
                return NotFound();

            var transactions = _bankService.GetTransactionsByAccount(account)
                .Select(x => new TransactionViewModel(x))
                .ToList();

            return OkResult(transactions);
        }
    }
}
