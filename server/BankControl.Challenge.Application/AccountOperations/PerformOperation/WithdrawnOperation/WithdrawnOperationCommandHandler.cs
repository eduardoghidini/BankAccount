using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.AccountOperations.PerformOperation.WithdrawnOperation
{
    public class WithdrawnOperationCommandHandler : IRequestHandler<WithdrawnOperationCommand, int>
    {
        private readonly IAccountOperationRequestRepository _accountOperationRequestRepository;

        private readonly IAccountOperationRepository _accountOperationRepository;

        private readonly NotificationContext _notificationContext;

        private readonly IUnitOfWork _unitOfWork;

        public WithdrawnOperationCommandHandler(
            IAccountOperationRequestRepository accountOperationRequestRepository,
            IAccountOperationRepository accountOperationRepository,
            NotificationContext notificationContext,
            IUnitOfWork unitOfWork)
        {
            _accountOperationRequestRepository = accountOperationRequestRepository;
            _accountOperationRepository = accountOperationRepository;
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;

        }

        public async Task<int> Handle(WithdrawnOperationCommand request, CancellationToken cancellationToken)
        {
            var accountRequest = await _accountOperationRequestRepository
                .GetByIdWithRelatedEntitiesAsync(request.OperationRequestId);

            if (accountRequest == null)
            {
                _notificationContext.AddNotification("RequestNotFound", "Request not found");
                return 0;
            }

            if (!accountRequest.AccountHasFounds)
            {
                _notificationContext.AddNotification("InsuficientFounds", "Account Has Insuficient founds");
                return 0;
            }
            accountRequest.Account.CurrentBalance -= accountRequest.Amount;

            var operation = accountRequest.GenerateOperation();

            operation.Validate();

            if (!operation.Valid)
            {
                _notificationContext.AddNotifications(operation.ValidationResult);
                return 0;
            }

            _accountOperationRepository.Add(operation);

            return await _unitOfWork.CommitAsync();
        }
    }
}
