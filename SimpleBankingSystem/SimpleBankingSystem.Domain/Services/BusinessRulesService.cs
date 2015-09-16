using SimpleBankingSystem.Domain.BusinessRules;
using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Service for business rules
    /// </summary>
    public class BusinessRulesService : IBusinessRulesService
    {
        /// <summary>
        /// Validates <see cref="Transaction"/> transaction
        /// </summary>
        /// <param name="userAccounts">Collection of the user's accounts</param>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        /// <returns>Collection of <see cref="BusinessError"/></returns>
        public IEnumerable<BusinessError> Validate(IEnumerable<Account> userAccounts, Transaction transaction)
        {
            if (!userAccounts.All(x => x.User == transaction.Account.User))
                throw new ArgumentException("Not all accounts belong to this user");

            var rules = GetBusinessRules(userAccounts, transaction);
            var errors = new List<BusinessError>();

            foreach (var rule in rules)
            {
                if(!rule.CanExecuteTransaction())
                    errors.AddRange(rule.GetErrors());
            }

            if (errors.Count > 0)
            {
                var sb = new StringBuilder();
                errors.ForEach(x => sb.AppendLine(x.Message));

                transaction.Message = sb.ToString();
            }

            return errors;
        }

        /// <summary>
        /// Gets business rules
        /// </summary>
        /// <param name="userAccounts">Collection of the user's accounts</param>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        /// <returns>Collection of <see cref="IBusinessRule"/></returns>
        private IEnumerable<IBusinessRule> GetBusinessRules(IEnumerable<Account> userAccounts, Transaction transaction)
        {
            yield return new AccountCantHaveLess100(transaction);
            yield return new UserCantWithdrawMoreThan90PercentTotalBalance(userAccounts, transaction);
            yield return new UserCantDepositMoreThan10000InTransaction(transaction);
        }
    }
}
