using MediatR;

namespace BankAccount.Warren.Application.Accounts.IncomeRentabilization
{
    public class IncomeRentabilizationCommand : IRequest<int>
    {
        public int AccountId { get; set; }
    }
}
