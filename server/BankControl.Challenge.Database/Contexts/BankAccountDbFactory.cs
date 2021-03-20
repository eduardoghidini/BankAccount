using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BankAccount.Warren.Database.Contexts
{
    class BankAccountDbFactory : IDesignTimeDbContextFactory<BankAccountDbContext>
    {
        public BankAccountDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BankAccountDbContext>();
            optionsBuilder.UseMySql("Server=localhost;User Id=user;Password=password;Database=db");

            return new BankAccountDbContext(optionsBuilder.Options);
        }
    }
}
