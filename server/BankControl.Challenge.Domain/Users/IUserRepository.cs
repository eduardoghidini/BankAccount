using BankAccount.Warren.Domain.Abstractions;
using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.Users
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> LoginAsync(string userName, string password);
    }
}
