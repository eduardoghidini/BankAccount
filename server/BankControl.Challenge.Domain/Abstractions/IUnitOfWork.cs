using System.Threading.Tasks;

namespace BankAccount.Warren.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}
