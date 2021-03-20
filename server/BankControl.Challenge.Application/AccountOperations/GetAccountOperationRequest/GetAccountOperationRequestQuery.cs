using BankAccount.Warren.Application.Abstractions;
using MediatR;

namespace BankAccount.Warren.Application.AccountOperations.GetAccountOperationRequest
{
    public class GetAccountOperationRequestQuery : IRequest<PagedResult<GetAccountOperationRequestQueryResult>>
    {
        public int AccountId { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}