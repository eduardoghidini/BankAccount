using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations.Validators;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Base;
using System;

namespace BankAccount.Warren.Domain.AccountOperations
{
    public class AccountOperation : EntityBase, IEntity
    {
        public OperationType OperationType { get; set; }

        public string Note { get; set; }

        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public DateTime OperationDate { get; set; }

        public virtual Account Account { get; set; }

        public bool Validate()
        {
            return Validate(this, new AccountOperationValidator());
        }
    }
}
