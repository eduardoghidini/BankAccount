using BankAccount.Warren.Domain.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.AccountOperations
{
    public interface IAccountOperationRequestRepository : IRepository<AccountOperationRequest>
    {
        Task<AccountOperationRequest> GetByIdWithRelatedEntitiesAsync(int id);

        Task<List<AccountOperationRequest>> ListAsync(int accountId, int pageSize, int pageNumber);
    }
}
