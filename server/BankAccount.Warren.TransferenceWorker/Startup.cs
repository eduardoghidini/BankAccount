using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BankAccount.Warren.Database;
using BankAccount.Warren.Application;
using BankAccount.Warren.HangfireMySqlJob;
using BankAccount.Warren.TransferenceWorker.Jobs;
using BankAccount.Warren.Domain.AccountOperations.Jobs;
using BankAccount.Warren.Application.Configurations;
using Microsoft.Extensions.Options;

namespace BankAccount.Warren.TransferenceWorker
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

            services.AddHangFireMySqlJobServer(Configuration.GetConnectionString("BankChallenge"));

            services.AddDatabase(Configuration.GetConnectionString("BankChallenge"));

            services.AddRepositories();

            services.AddApplicationCQRS();

            AddJobExecutions(services);
        }

        private IServiceCollection AddJobExecutions(IServiceCollection services)
        {
            return services.AddTransient<IPerformOperationRequestJob, PerformOperationRequestJob>();
        }
    }
}
