using BankAccount.Warren.Application.AccountOperations.PerformOperation.Factory;
using MediatR;

namespace BankAccount.Warren.Application.AccountOperations.PerformOperation.DepositOperation
{
    public class DepositOperationCommand : IOperation, IRequest<int>
    {
        public int OperationRequestId { get; set; }
    }
}
