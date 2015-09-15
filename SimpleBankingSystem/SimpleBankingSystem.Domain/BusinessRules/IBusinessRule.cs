using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.BusinessRules
{
    /// <summary>
    /// A business rule for validation of the transaction executing
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// Checks that the transaction can be executed
        /// </summary>
        /// <returns>TRUE - if transaction can be executed, FALSE - else</returns>
        Boolean CanExecuteTransaction();

        /// <summary>
        /// Gets errors after a validation of the transaction
        /// </summary>
        /// <returns></returns>
        IEnumerable<BusinessError> GetErrors();
    }
}
