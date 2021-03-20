using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.Accounts.GetAccount
{
    public class GetAccountQueryHandler : IRequestHandler<GetAccountQuery, GetAccountResult>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly NotificationContext _notificationContext;

        public GetAccountQueryHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        }

        public async Task<GetAccountResult> Handle(GetAccountQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(request?.AccountId ?? 0);

            if (account == null)
            {
                _notificationContext.AddNotification("NotFound", "Account not found");
                return new GetAccountResult();
            }

            return new GetAccountResult()
            {
                AccountId = account.Id,
                CurrentAmount = account.CurrentBalance,
                AccountNumber = account.AccountNumber,
                Name = account.OwnerName,
            };
        }
    }
}