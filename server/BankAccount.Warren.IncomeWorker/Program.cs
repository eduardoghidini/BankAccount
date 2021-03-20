using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BankAccount.Warren.IncomeWorker
{
    class Program
    {
        public static int Main(string[] args)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
                ?? Environments.Development;

            var configurationBuilder = new ConfigurationBuilder();
            ConfigureConfiguration(configurationBuilder, environment);

            var configuration = configurationBuilder.Build();

            var startup = new Startup(configuration);

            try
            {
                CreateHostBuilder(args, startup).Build().Run();
                return 0;
            }
            catch (Exception ex)
            {
                return 1;
            }
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args, Startup startup) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var environment = hostingContext.HostingEnvironment.EnvironmentName;
                    ConfigureConfiguration(config, environment);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    startup.ConfigureServices(services);
                    services.AddHostedService<Worker>();
                });

        private static void ConfigureConfiguration(IConfigurationBuilder builder, string environment)
        {
            builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            builder.AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true);
            builder.AddEnvironmentVariables("App_");
        }
    }
}
