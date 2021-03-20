using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Domain.Abstractions;
using System.Threading.Tasks;

namespace BankAccount.Warren.Database.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private BankAccountDbContextFactory _dbFactory;

        public UnitOfWork(BankAccountDbContextFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        public Task<int> CommitAsync()
        {
            return _dbFactory.DbContext.SaveChangesAsync();
        }
    }

}
