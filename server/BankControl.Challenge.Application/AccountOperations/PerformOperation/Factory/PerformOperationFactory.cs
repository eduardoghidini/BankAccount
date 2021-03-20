using BankAccount.Warren.Application.AccountOperations.PerformOperation.DepositOperation;
using BankAccount.Warren.Application.AccountOperations.PerformOperation.PaymentOperation;
using BankAccount.Warren.Application.AccountOperations.PerformOperation.WithdrawnOperation;
using BankAccount.Warren.Domain.AccountOperations;

namespace BankAccount.Warren.Application.AccountOperations.PerformOperation.Factory
{
    public static class PerformOperationFactory
    {
        public static IOperation Factory(OperationType operationType)
        {
            return operationType switch
            {
                OperationType.Deposit => new DepositOperationCommand(),
                OperationType.Withdrawn => new WithdrawnOperationCommand(),
                OperationType.Payment => new PaymentOperationCommand(),
                _ => null
            };
        }
    }
}
