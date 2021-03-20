using BankAccount.Warren.Database.Base;
using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Domain.AccountOperations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Warren.Database.Repositories
{
    public class AccountOperationRepository : Repository<AccountOperation>, IAccountOperationRepository
    {
        public AccountOperationRepository(BankAccountDbContextFactory dbFactory)
            : base(dbFactory)
        {
        }

        public Task<List<AccountOperation>> ListAsync(int accountId, int pageSize, int pageNumber)
        {
            return DbSet
                .Where(_ => _.AccountId == accountId)
                .OrderByDescending(_ => _.OperationDate)
                .Skip(pageSize * pageNumber)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}
