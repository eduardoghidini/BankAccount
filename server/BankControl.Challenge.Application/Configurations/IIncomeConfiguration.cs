namespace BankAccount.Warren.Application.Configurations
{
    public interface IIncomeConfiguration
    {
        int AnualFactor { get; set; }

        double CIDPercentual { get; set; }
    }
}
