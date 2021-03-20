using BankAccount.Warren.Database.Base;
using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Domain.AccountOperations;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Warren.Database.Repositories
{
    public class AccountOperationRequestRepository : Repository<AccountOperationRequest>, IAccountOperationRequestRepository
    {
        public AccountOperationRequestRepository(BankAccountDbContextFactory dbFactory)
            : base(dbFactory)
        {

        }

        public Task<AccountOperationRequest> GetByIdWithRelatedEntitiesAsync(int id)
        {
            return DbSet
                    .Include(_ => _.Account)
                    .FirstOrDefaultAsync(_ => _.Id == id);
        }

        public Task<List<AccountOperationRequest>> ListAsync(int accountId, int pageSize, int pageNumber)
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
