using BankAccount.Warren.Application.Configurations;

namespace BankAccount.Warren.Api.Configurations
{
    public class IncomeConfiguration : IIncomeConfiguration
    {
        public int AnualFactor { get; set; }

        public double CIDPercentual { get; set; }
    }
}
