using BankAccount.Warren.Domain.Abstractions;
using Hangfire;
using Hangfire.MySql.Core;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccount.Warren.HangfireMySqlJob
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddHangFireMySqlJobServer(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(x => x.UseStorage(new MySqlStorage(connectionString)));

            services.AddHangfireServer();

            return services;
        }

        public static IServiceCollection AddHangFireJobClient(this IServiceCollection services, string connectionString)
        {
            services.AddHangfire(x => x.UseStorage(new MySqlStorage(connectionString)));
            services.AddTransient<IJobClient, HangfireBackgroundJobClient>();
            return services;
        }
    }
}
