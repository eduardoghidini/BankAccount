using BankAccount.Warren.Domain.AccountOperations;
using System;

namespace BankAccount.Warren.Application.AccountOperations.GetAccountOperationRequest
{
    public class GetAccountOperationRequestQueryResult
    {
        public int Id { get; set; }

        public DateTime RequestedDate { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public OperationType Type { get; set; }

        public string OperationResponseMessage { get; set; }

        public AccountOperationStatus Status { get; set; }

        public decimal Amount { get; set; }
    }
}
