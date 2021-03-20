using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.IncomeWorker
{
    public class Worker : BackgroundService
    {
        public Worker()
        {
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.FromResult(0);
        }
    }
}
