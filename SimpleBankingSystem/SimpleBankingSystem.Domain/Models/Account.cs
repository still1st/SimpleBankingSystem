using System;

namespace SimpleBankingSystem.Domain.Models
{
    /// <summary>
    /// Account entity
    /// </summary>
    public class Account
    {
        /// <summary>
        /// Gets or sets the account id
        /// </summary>
        public Int64 AccountId { get; set; }

        /// <summary>
        /// Gets or sets the balance
        /// </summary>
        public Decimal Balance { get; set; }

        /// <summary>
        /// Gets or sets the owner of the account
        /// </summary>
        public User User { get; set; }
    }
}
