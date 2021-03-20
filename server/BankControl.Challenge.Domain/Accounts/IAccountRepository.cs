using BankAccount.Warren.Domain.Abstractions;
using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.Accounts
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetByAccountNumberAsync(string accountNumber);
    }
}
