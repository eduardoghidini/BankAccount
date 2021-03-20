using BankAccount.Warren.Database.Base;
using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BankAccount.Warren.Database.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(BankAccountDbContextFactory dbFactory)
            : base(dbFactory)
        {
        }

        public Task<Account> GetByAccountNumberAsync(string accountNumber)
        {
            return DbSet.FirstOrDefaultAsync(_ => _.AccountNumber == accountNumber);
        }
    }
}
