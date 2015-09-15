using SimpleBankingSystem.Domain.Models;
using System;

namespace SimpleBankingSystem.Domain.BusinessRules
{
    /// <summary>
    /// Error which can be threw with transaction executing
    /// </summary>
    public class BusinessError
    {
        /// <summary>
        /// The error message
        /// </summary>
        public String Message { get; private set; }

        /// <summary>
        /// The transaction
        /// </summary>
        public Int64 TransactionId { get; private set; }

        /// <summary>
        /// Initiales a new 
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="transactionId">Id of the transaction</param>
        public BusinessError(Int64 transactionId, String message)
        {
            TransactionId = transactionId; 
            Message = message;
        }
    }
}
