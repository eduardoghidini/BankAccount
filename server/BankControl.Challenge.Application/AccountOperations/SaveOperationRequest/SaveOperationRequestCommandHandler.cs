using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.AccountOperations.Jobs;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.AccountOperations.SaveOperationRequest
{
    public class SaveOperationRequestCommandHandler : IRequestHandler<SaveOperationRequestCommand, int>
    {
        private readonly IJobClient _jobClient;

        private readonly IAccountOperationRequestRepository _accountOperationRequestRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly NotificationContext _notificationContext;

        private readonly IAccountRepository _accountRepository;

        public SaveOperationRequestCommandHandler(
            IJobClient jobClient,
            IAccountOperationRequestRepository accountOperationRequestRepository,
            IUnitOfWork unitOfWork,
            IAccountRepository accountRepository,
            NotificationContext notificationContext)
        {
            _jobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _accountOperationRequestRepository = accountOperationRequestRepository ?? throw new ArgumentNullException(nameof(accountOperationRequestRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _notificationContext = notificationContext ?? throw new ArgumentNullException(nameof(notificationContext));
        }

        public async Task<int> Handle(SaveOperationRequestCommand request, CancellationToken cancellationToken)
        {
            var operationRequest = new AccountOperationRequest()
            {
                AccountId = request.AccountId,
                CreationDate = DateTime.UtcNow,
                Note = request.Note,
                OperationType = request.Type,
                OperationDate = request.OperationDate,
                Amount = request.Amount,
                Status = AccountOperationStatus.Created,
            };

            operationRequest.Validate();

            if (!operationRequest.Valid)
            {
                _notificationContext.AddNotifications(operationRequest.ValidationResult);
                return default(int);
            }

            _accountOperationRequestRepository.Add(operationRequest);

            await _unitOfWork.CommitAsync();

            operationRequest.JobReferenceId = _jobClient.Enqueue<IPerformOperationRequestJob>
                (job => job.PerformOperation(operationRequest.Id),
                request.OperationDate);

            _accountOperationRequestRepository.Update(operationRequest);

            await _unitOfWork.CommitAsync();

            return operationRequest.Id;
        }
    }
}
