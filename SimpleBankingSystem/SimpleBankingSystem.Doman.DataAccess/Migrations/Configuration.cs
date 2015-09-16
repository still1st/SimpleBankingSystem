namespace SimpleBankingSystem.Doman.DataAccess.Migrations
{
    using SimpleBankingSystem.Domain.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SimpleBankingSystemContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(SimpleBankingSystemContext context)
        {
            var user = new User
            {
                FirstName = "John",
                LastName = "Johnson"
            };

            context.Users.AddOrUpdate(x => x.LastName, user);
            context.SaveChanges();
        }
    }
}
