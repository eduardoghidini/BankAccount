using BankAccount.Warren.Database.Base;
using BankAccount.Warren.Database.Contexts;
using BankAccount.Warren.Database.Repositories;
using BankAccount.Warren.Domain.Abstractions;
using BankAccount.Warren.Domain.AccountOperations;
using BankAccount.Warren.Domain.Accounts;
using BankAccount.Warren.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BankAccount.Warren.Database
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAccountOperationRepository, AccountOperationRepository>();
            services.AddTransient<IAccountOperationRequestRepository, AccountOperationRequestRepository>();
            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<BankAccountDbContext>(options =>
            {
                options.UseMySql(connectionString);
            });

            services.AddScoped<Func<BankAccountDbContext>>((provider) => () => provider.GetService<BankAccountDbContext>());
            services.AddScoped<BankAccountDbContextFactory>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
