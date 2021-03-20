using BankAccount.Warren.Application.Configurations;

namespace BankAccount.Warren.IncomeWorker.Configurations
{
    public class IncomeConfiguration : IIncomeConfiguration
    {
        public int AnualFactor { get; set; }

        public double CIDPercentual { get; set; }
    }
}
