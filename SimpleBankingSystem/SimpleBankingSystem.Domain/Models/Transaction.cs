using System;

namespace SimpleBankingSystem.Domain.Models
{
    /// <summary>
    /// Transaction entity
    /// </summary>
    public class Transaction
    {
        /// <summary>
        /// Gets or sets the transaction id
        /// </summary>
        public Int64 TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the transacton type
        /// </summary>
        public TransactionType Type { get; set; }

        /// <summary>
        /// Gets or sets the amount
        /// </summary>
        public Decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the account
        /// </summary>
        public virtual Account Account { get; set; }

        /// <summary>
        /// Gets or sets the info message about the transaction
        /// </summary>
        public String Message { get; set; }

        /// <summary>
        /// Gets or sets the flag that the transaction is commited
        /// </summary>
        public Boolean IsCommited { get; set; }
    }
}
