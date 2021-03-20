using BankAccount.Warren.Application.Configurations;
using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.Accounts.IncomeRentabilization
{
    public class IncomeRentabilizationCommandHandler : IRequestHandler<IncomeRentabilizationCommand, int>
    {

        private readonly IIncomeConfiguration _incomeConfiguration;

        private readonly IAccountRepository _accountRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IAccountOperationRepository _accountOperationRepository;

        private readonly NotificationContext _notificationContext;

        public IncomeRentabilizationCommandHandler(
            IIncomeConfiguration incomeConfiguration,
            IAccountRepository accountRepository,
            IUnitOfWork unitOfWork,
            IAccountOperationRepository accountOperationRepository,
            NotificationContext notificationContext
            )
        {
            _incomeConfiguration = incomeConfiguration;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _accountOperationRepository = accountOperationRepository;
            _notificationContext = notificationContext;
        }

        public async Task<int> Handle(IncomeRentabilizationCommand request, CancellationToken cancellationToken)
        {
            /*
             Note: reference for fórmula :
                https://blog.paranabanco.com.br/investimento/cdi-diario/#:~:text=somente%20entre%20bancos.-,Como%20%C3%A9%20feito%20o%20c%C3%A1lculo,das%20taxas%20das%20transa%C3%A7%C3%B5es%20interbanc%C3%A1rias.

               Access Date: 18/03/2021
            */
            var account = await _accountRepository.GetByIdAsync(request.AccountId);

            if (account == null)
            {
                _notificationContext.AddNotification("AccountNotFound", "Account Not Found");
                return 0;
            }

            if (account.CurrentBalance == 0)
            {
                _notificationContext.AddNotification("NoBalance", "Current balance is 0");
                return 0;
            }

            var cidFactor = 1 + (_incomeConfiguration.CIDPercentual / 100);

            var anualFactor = (double)1 / _incomeConfiguration.AnualFactor;

            var amountOperation = (decimal)Math.Pow(cidFactor, anualFactor) * account.CurrentBalance;

            _accountOperationRepository.Add(new AccountOperation()
            {
                AccountId = account.Id,
                Note = "Transação de rendimento diário",
                Amount = amountOperation - account.CurrentBalance,
                OperationType = OperationType.IncomeRentabilization,
                OperationDate = DateTime.Now
            }); ;

            account.CurrentBalance = amountOperation;

            return await _unitOfWork.CommitAsync();
        }
    }
}