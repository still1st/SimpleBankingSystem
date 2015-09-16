using SimpleBankingSystem.Domain.Models;
using System;
using System.Collections.Generic;

namespace SimpleBankingSystem.Domain.BusinessRules
{
    /// <summary>
    /// Base class for business rules
    /// </summary>
    public abstract class BusinessRuleBase : IBusinessRule
    {
        /// <summary>
        /// The transaction
        /// </summary>
        protected readonly Transaction Transaction;

        /// <summary>
        /// The collection of the errors
        /// </summary>
        private readonly List<BusinessError> _errors;

        /// <summary>
        /// Initiales a new instance of <see cref="BusinessRuleBase"/>
        /// </summary>
        /// <param name="transaction"></param>
        public BusinessRuleBase(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("transaction");

            Transaction = transaction;
            _errors = new List<BusinessError>();
        }

        /// <summary>
        /// Checks that the transaction can be executed
        /// </summary>
        /// <param name="transaction"><see cref="Transaction"/> entities</param>
        /// <returns>TRUE - if transaction can be executed, FALSE - else</returns>
        public abstract Boolean CanExecuteTransaction();

        /// <summary>
        /// Gets the errors
        /// </summary>
        /// <returns>Collection of the errors</returns>
        public IEnumerable<BusinessError> GetErrors()
        {
            return _errors;
        }

        /// <summary>
        /// Adds error in errors collection
        /// </summary>
        /// <param name="message">Message of the error</param>
        protected void AddError(String message)
        {
            if (String.IsNullOrEmpty(message))
                throw new ArgumentNullException("message");

            _errors.Add(new BusinessError(Transaction.TransactionId, message));
        }
    }
}
