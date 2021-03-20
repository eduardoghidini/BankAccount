using MediatR;

namespace BankAccount.Warren.Application.AccountOperations.CancelOperationRequest
{
    public class CancelOperationRequestCommand : IRequest<bool>
    {
        public int OperationId { get; set; }

        public int AccountId { get; set; }
    }
}
