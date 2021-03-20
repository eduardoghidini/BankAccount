using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.AccountOperations.Jobs
{
    public interface IPerformOperationRequestJob
    {
        Task PerformOperation(int operationId);
    }
}
