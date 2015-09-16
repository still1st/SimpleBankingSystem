using SimpleBankingSystem.Domain.Models;
using SimpleBankingSystem.Doman.DataAccess.Migrations;
using System.Data.Entity;

namespace SimpleBankingSystem.Doman.DataAccess
{
    /// <summary>
    /// DataBase context
    /// </summary>
    public class SimpleBankingSystemContext : DbContext
    {
        /// <summary>
        /// Set of <see cref="User"/> entities
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Sets of <see cref="Account"/> entities
        /// </summary>
        public DbSet<Account> Accounts { get; set; }

        /// <summary>
        /// Set of <see cref="Transaction"/> entities
        /// </summary>
        public DbSet<Transaction> Transactions { get; set; }

        /// <summary>
        /// Initializes a new instance of <see cref="SimpleBankingSystemContext"/>
        /// </summary>
        public SimpleBankingSystemContext()
            :base("DefaultConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SimpleBankingSystemContext, Configuration>());
        }
    }
}
