using SimpleBankingSystem.Domain.BusinessRules;
using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.Services
{
    /// <summary>
    /// Service for business rules
    /// </summary>
    public interface IBusinessRulesService
    {
        /// <summary>
        /// Validates <see cref="Transaction"/> transaction
        /// </summary>
        /// <param name="userAccounts">Collection of the user's accounts</param>
        /// <param name="transaction"><see cref="Transaction"/> entity</param>
        /// <returns>Collection of <see cref="BusinessError"/></returns>
        IEnumerable<BusinessError> Validate(IEnumerable<Account> userAccounts, Transaction transaction);
    }
}
