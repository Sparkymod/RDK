using Microsoft.EntityFrameworkCore;
using RDK.Database.Core;
using RDK.Database.Models;

namespace RDK.Database
{
    public class DatabaseContext : DbContext
    {
        // Add all Model entities here.
        public DbSet<Account> Accounts { get; set; }

        public DatabaseContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Account>(Account.Build);
        }
    }
}
