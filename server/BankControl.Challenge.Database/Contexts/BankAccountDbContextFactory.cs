using System;

namespace BankAccount.Warren.Database.Contexts
{
    public class BankAccountDbContextFactory : IDisposable
    {
        private bool _disposed;

        private Func<BankAccountDbContext> _instanceFunc;

       
        private BankAccountDbContext _dbContext;

        public BankAccountDbContext DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public BankAccountDbContextFactory(Func<BankAccountDbContext> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
