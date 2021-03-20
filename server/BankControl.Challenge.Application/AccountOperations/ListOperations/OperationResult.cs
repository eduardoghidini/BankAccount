using BankAccount.Warren.Domain.AccountOperations;
using System;

namespace BankAccount.Warren.Application.AccountOperations.ListOperations
{
    public class OperationResult
    {
        public OperationType Type { get; set; }

        public decimal Amount { get; set; }

        public string Note { get; set; }

        public DateTime OperationDate { get; set; }
    }
}
