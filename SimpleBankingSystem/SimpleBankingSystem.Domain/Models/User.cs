using System;

namespace SimpleBankingSystem.Domain.Models
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the user id
        /// </summary>
        public Int64 UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name
        /// </summary>
        public String FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name
        /// </summary>
        public String LastName { get; set; }
    }
}
