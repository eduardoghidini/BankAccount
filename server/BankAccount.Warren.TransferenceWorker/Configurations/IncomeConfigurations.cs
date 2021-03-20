using BankAccount.Warren.Application.Configurations;

namespace BankAccount.Warren.TransferenceWorker
{
    public class IncomeConfiguration : IIncomeConfiguration
    {
        public int AnualFactor { get; set; }

        public double CIDPercentual { get; set; }
    }
}
