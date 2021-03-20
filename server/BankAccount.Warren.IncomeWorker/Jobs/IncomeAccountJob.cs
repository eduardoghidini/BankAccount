using BankAccount.Warren.Application.Accounts.IncomeRentabilization;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BankAccount.Warren.IncomeWorker.Jobs
{
    public class IncomeAccountJob
    {
        private readonly IMediator _mediator;

        private readonly NotificationContext _notificationContext;

        private readonly IAccountRepository _accountRepository;

        public IncomeAccountJob(
            IMediator mediator,
            NotificationContext notificationContext,
            IAccountRepository accountRepository
            )
        {
            _mediator = mediator;
            _notificationContext = notificationContext;
            _accountRepository = accountRepository;
        }

        public async Task Execute()
        {

            try
            {
                var accounts = await _accountRepository.ListAllAsync();

                foreach (var item in accounts)
                {
                    try
                    {
                        await _mediator.Send(new IncomeRentabilizationCommand()
                        {
                            AccountId = item.Id,
                        });
                    }
                    catch (Exception exx)
                    {
                        //LOG : DO SOMETHING
                    }
                }
            }
            catch (Exception EX)
            {
                //LOG : DO SOMETHING
            }
            return;
        }
    }
}
