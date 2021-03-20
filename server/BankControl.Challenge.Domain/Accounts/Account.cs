using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Base;
using BankAccount.Warren.Domain.Users;
using System.Collections.Generic;

namespace BankAccount.Warren.Domain.Accounts
{
    public class Account : EntityBase, IEntity
    {
        public string OwnerName { get; set; }

        public string AccountNumber { get; set; }

        public decimal CurrentBalance { get; set; }

        public decimal ApplyiedBalance { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual List<AccountOperation> AccountOperations { get; set; }

        public virtual List<AccountOperationRequest> AccountOperationRequest { get; set; }
    }
}