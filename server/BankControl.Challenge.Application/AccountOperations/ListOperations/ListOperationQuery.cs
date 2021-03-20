using BankAccount.Warren.Application.Abstractions;
using MediatR;

namespace BankAccount.Warren.Application.AccountOperations.ListOperations
{
    public class ListOperationQuery : IRequest<PagedResult<OperationResult>>
    {
        public int AccountId { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
