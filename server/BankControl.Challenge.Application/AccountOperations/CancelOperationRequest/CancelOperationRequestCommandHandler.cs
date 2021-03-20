using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.AccountOperations.CancelOperationRequest
{
    public class CancelOperationRequestCommandHandler : IRequestHandler<CancelOperationRequestCommand, bool>
    {
        private readonly IJobClient _jobClient;

        private readonly IAccountOperationRequestRepository _accountOperationRequestRepository;

        private readonly IUnitOfWork _unitOfWork;

        private readonly NotificationContext _notificationContext;

        public CancelOperationRequestCommandHandler(
            IJobClient jobClient,
            IAccountOperationRequestRepository accountOperationRequestRepository,
            IUnitOfWork unitOfWork,
            NotificationContext notificationContext)
        {
            _jobClient = jobClient ?? throw new ArgumentNullException(nameof(jobClient));
            _accountOperationRequestRepository = accountOperationRequestRepository ?? throw new ArgumentNullException(nameof(accountOperationRequestRepository));
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _notificationContext = notificationContext ?? throw new ArgumentNullException(nameof(notificationContext));
        }

        public async Task<bool> Handle(CancelOperationRequestCommand request, CancellationToken cancellationToken)
        {
            var requestOperation = await _accountOperationRequestRepository
                .GetByIdAsync(request.OperationId);

            if (requestOperation == null)
            {
                _notificationContext.AddNotification("NotFound", "Requested data not found");
                return false;
            }

            if (request.AccountId != requestOperation.AccountId)
            {
                _notificationContext.AddNotification("IntegrityViolation", "Can't access requested data");
                return false;
            }

            if (!requestOperation.CanCancelJob)
            {
                _notificationContext.AddNotification("ExecutedOperation", "Can't cancel operation because it was already executed.");
                return false;
            }

            return _jobClient.Delete(requestOperation.JobReferenceId);
        }
    }
}
