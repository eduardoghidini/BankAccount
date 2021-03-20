using BankAccount.Warren.Domain.AccountOperations;
using MediatR;
using System;

namespace BankAccount.Warren.Application.AccountOperations.SaveOperationRequest
{
    public class SaveOperationRequestCommand : IRequest<int>
    {
        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public OperationType Type { get; set; }

        public string Note { get; set; }

        public DateTime OperationDate { get; set; }
    }
}