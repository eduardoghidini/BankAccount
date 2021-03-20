using BankAccount.Warren.Domain.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.AccountOperations
{
    public interface IAccountOperationRepository : IRepository<AccountOperation>
    {
        Task<List<AccountOperation>> ListAsync(int accountId, int pageSize, int pageNumber);
    }
}
