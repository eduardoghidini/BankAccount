using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations.Validators;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Base;
using System;

namespace BankAccount.Warren.Domain.AccountOperations
{
    public class AccountOperationRequest : EntityBase, IEntity
    {
        public OperationType OperationType { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime OperationDate { get; set; }

        public AccountOperationStatus Status { get; set; }

        public string Note { get; set; }

        public int AccountId { get; set; }

        public string OperationResponseMessage { get; set; }

        public string JobReferenceId { get; set; }

        public DateTime? ProcessedDate { get; set; }

        public bool CanCancelJob { get => Status == AccountOperationStatus.Created && !ProcessedDate.HasValue; }

        public bool AccountHasFounds { get => Account == null | Account.CurrentBalance < Amount ? false : true; }

        public virtual Account Account { get; set; }

        public AccountOperation GenerateOperation()
        {
            return new AccountOperation()
            {
                AccountId = AccountId,
                Amount = Amount,
                Note = Note,
                OperationType = OperationType,
                OperationDate = DateTime.Now
            };
        }
        public bool Validate()
        {
            return Validate(this, new AccountOperationRequestValidator());
        }
    }
}