using BankAccount.Warren.Application.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.AccountOperations.GetAccountOperationRequest
{
    public class GetAccountOperationRequestQueryHadler : IRequestHandler<GetAccountOperationRequestQuery, PagedResult<GetAccountOperationRequestQueryResult>>
    {
        private readonly IAccountOperationRequestRepository _accountRequestRepository;

        private readonly NotificationContext _notificationContext;

        public GetAccountOperationRequestQueryHadler(
            IAccountOperationRequestRepository accountRequestRepository,
            NotificationContext notificationContext)
        {
            _accountRequestRepository = accountRequestRepository;
            _notificationContext = notificationContext;

        }

        public async Task<PagedResult<GetAccountOperationRequestQueryResult>> Handle(GetAccountOperationRequestQuery request, CancellationToken cancellationToken)
        {
            if (request.PageSize < 0 || request.Page < 0)
            {
                _notificationContext.AddNotification("PaginationError", "Pagination information is in wrong formta.");
            }

            var list = await _accountRequestRepository.ListAsync(request.AccountId, request.PageSize, request.Page);

            var listSize = await _accountRequestRepository.CountAsync(_ => _.AccountId == request.AccountId);

            return new PagedResult<GetAccountOperationRequestQueryResult>()
            {
                TotalPages = listSize,
                Data = list.Select(_ => new GetAccountOperationRequestQueryResult()
                {
                    Amount = _.Amount,
                    Id = _.Id,
                    OperationResponseMessage = _.OperationResponseMessage,
                    Type = _.OperationType,
                    ProcessedDate = _.ProcessedDate,
                    RequestedDate = _.CreationDate,
                    Status = _.Status,

                }).ToList(),
                PageNumber = request.Page,
                PageSize = request.PageSize
            };
        }
    }
}