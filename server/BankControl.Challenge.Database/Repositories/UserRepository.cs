using BankAccount.Warren.Database.Base;
using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Domain.Users;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Warren.Database.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BankAccountDbContextFactory dbFactory)
            : base(dbFactory)
        {
        }

        public Task<User> LoginAsync(string userName, string password)
        {
            return DbSet
                .Include(_ => _.Account)
                .FirstOrDefaultAsync(_ => _.UserName == userName && _.Password == password);
        }
    }
}
