using MediatR;

namespace BankAccount.Warren.Application.Accounts.GetAccount
{
    public class GetAccountQuery : IRequest<GetAccountResult>
    {
        public int AccountId { get; set; }
    }
}
