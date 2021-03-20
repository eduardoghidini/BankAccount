using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Base;

namespace BankAccount.Warren.Domain.Users
{
    public class User : EntityBase, IEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public virtual Account Account { get; set; }
    }
}
