using BankAccount.Warren.Database.Mappings;
using BankAccount.Warren.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace BankAccount.Warren.Database.Contexts
{
    public class BankAccountDbContext : DbContext
    {
        public BankAccountDbContext(DbContextOptions<BankAccountDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;User Id=user;Password=password;Database=db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountMapping());
            modelBuilder.ApplyConfiguration(new AccountOperationRequestMapping());
            modelBuilder.ApplyConfiguration(new AccountOperationMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
        }
    }
}