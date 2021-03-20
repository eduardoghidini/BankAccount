using BankAccount.Warren.Domain.AccountOperations;
using System;

namespace BankAccount.Warren.Api.Models.Operations
{
    public class OperationResponse
    {
        public OperationType Type { get; set; }

        public decimal Amount { get; set; }

        public string Note { get; set; }

        public DateTime Date { get; set; }
    }
}
