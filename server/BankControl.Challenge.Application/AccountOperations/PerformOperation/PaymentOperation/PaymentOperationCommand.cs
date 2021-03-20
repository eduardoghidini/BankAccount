using BankAccount.Warren.Application.AccountOperations.PerformOperation.Factory;
using MediatR;

namespace BankAccount.Warren.Application.AccountOperations.PerformOperation.PaymentOperation
{
    public class PaymentOperationCommand : IRequest<int>, IOperation
    {
        public int OperationRequestId { get; set; }
    }
}
