using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankAccount.Warren.Database;
using BankAccount.Warren.Application;
using BankAccount.Warren.HangfireMySqlJob;
using Hangfire;
using System;
using Hangfire.MySql.Core;
using BankAccount.Warren.IncomeWorker.Jobs;
using Hangfire.Storage;
using Microsoft.Extensions.Options;
using BankAccount.Warren.Application.Configurations;
using BankAccount.Warren.IncomeWorker.Configurations;

namespace BankAccount.Warren.IncomeWorker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IncomeConfiguration>(Configuration.GetSection("Income"));

            services.AddSingleton<IIncomeConfiguration>(f => f.GetRequiredService<IOptions<IncomeConfiguration>>().Value);

            services.AddHangFireMySqlJobServer(Configuration.GetConnectionString("HangFire"));

            services.AddHangFireJobClient(Configuration.GetConnectionString("HangFire"));

            services.AddDatabase(Configuration.GetConnectionString("BankChallenge"));

            services.AddRepositories();

            services.AddApplicationCQRS();

            services.AddTransient<IncomeAccountJob>();

            JobStorage.Current = new MySqlStorage(Configuration.GetConnectionString("HangFire"));

            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }

            RecurringJob.AddOrUpdate<IncomeAccountJob>(x => x.Execute(), Cron.Daily);
        }
    }
}
