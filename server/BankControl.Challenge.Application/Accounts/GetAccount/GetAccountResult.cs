using System;

namespace BankAccount.Warren.Application.Accounts.GetAccount
{
    public class GetAccountResult
    {
        public int AccountId { get; set; }

        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public decimal CurrentAmount { get; set; }
    }
}