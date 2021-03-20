using BankAccount.Warren.Application.AccountOperations.PerformOperation.Factory;
using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.AccountOperations.Jobs;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading.Tasks;

namespace BankAccount.Warren.TransferenceWorker.Jobs
{
    public class PerformOperationRequestJob : IPerformOperationRequestJob
    {
        private readonly IAccountOperationRequestRepository _accountOperationRequestRepository;

        private readonly IMediator _mediator;

        private readonly NotificationContext _notificationContext;

        private readonly IUnitOfWork _unitOfWork;

        public PerformOperationRequestJob(
            IAccountOperationRequestRepository accountOperationRequestRepository,
            IMediator mediator,
            NotificationContext notificationContext,
            IUnitOfWork unitOfWork)
        {
            _accountOperationRequestRepository = accountOperationRequestRepository;
            _mediator = mediator;
            _notificationContext = notificationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task PerformOperation(int operationId)
        {
            var operationRequest = await _accountOperationRequestRepository.GetByIdAsync(operationId);

            try
            {
                var command = PerformOperationFactory.Factory(operationRequest.OperationType);

                command.OperationRequestId = operationId;

                var result = await _mediator.Send(command);

                //note: query operation request again to get updated data.

                operationRequest = await _accountOperationRequestRepository.GetByIdAsync(operationId);

                if (_notificationContext.HasNotifications)
                {
                    operationRequest.Status = AccountOperationStatus.Error;
                    operationRequest.OperationResponseMessage = _notificationContext.ConcatenedMessages;
                }
                else
                {
                    operationRequest.Status = AccountOperationStatus.Sucess;
                    operationRequest.ProcessedDate = DateTime.Now;
                }

            }
            catch (Exception ex)
            {
                operationRequest.Status = AccountOperationStatus.Error;
                operationRequest.OperationResponseMessage = ex.Message;
            }
            finally
            {
                operationRequest.ProcessedDate = DateTime.Now;
                await _unitOfWork.CommitAsync();
            }

            return;
        }
    }
}
