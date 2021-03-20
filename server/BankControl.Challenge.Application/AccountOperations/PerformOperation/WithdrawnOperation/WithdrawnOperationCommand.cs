using BankAccount.Warren.Application.AccountOperations.PerformOperation.Factory;
using MediatR;

namespace BankAccount.Warren.Application.AccountOperations.PerformOperation.WithdrawnOperation
{
    public class WithdrawnOperationCommand : IOperation, IRequest<int>
    {
        public int OperationRequestId { get; set; }
    }
}
