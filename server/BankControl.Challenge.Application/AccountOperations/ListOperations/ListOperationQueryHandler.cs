using BankAccount.Warren.Application.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.AccountOperations.ListOperations
{
    public class ListOperationQueryHandler : IRequestHandler<ListOperationQuery, PagedResult<OperationResult>>
    {
        private readonly IAccountOperationRepository _accountOperationepository;

        private readonly NotificationContext _notificationContext;

        public ListOperationQueryHandler(
            IAccountOperationRepository accountOperationepository,
            NotificationContext notificationContext)
        {
            _accountOperationepository = accountOperationepository ?? throw new ArgumentNullException(nameof(accountOperationepository));
            _notificationContext = notificationContext ?? throw new ArgumentNullException(nameof(notificationContext));
        }

        public async Task<PagedResult<OperationResult>> Handle(ListOperationQuery request, CancellationToken cancellationToken)
        {
            if (request.PageNumber < 0 || request.PageSize < 1)
            {
                _notificationContext.AddNotification("PaginationError", "Pagination information is in wrong formta.");
            }

            var list = await _accountOperationepository.ListAsync(request.AccountId, request.PageSize, request.PageNumber);

            var listSize = await _accountOperationepository.CountAsync(_ => _.AccountId == request.AccountId);

            return new PagedResult<OperationResult>()
            {
                TotalPages = listSize,
                Data = list.Select(_ => new OperationResult()
                {
                    Amount = _.Amount,
                    Note = _.Note,
                    Type = _.OperationType,
                    OperationDate = _.OperationDate
                }).ToList(),
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
