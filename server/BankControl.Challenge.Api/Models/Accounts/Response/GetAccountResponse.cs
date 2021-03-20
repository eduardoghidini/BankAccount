namespace BankAccount.Warren.Api.Models.Accounts.Response
{
    public class GetAccountResponse
    {
        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public decimal CurrentAmount { get; set; }
    }
}
