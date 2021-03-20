using FluentValidation;

namespace BankAccount.Warren.Domain.AccountOperations.Validators
{
    public class AccountOperationValidator : AbstractValidator<AccountOperation>
    {
        public AccountOperationValidator()
        {
            RuleFor(a => a.Note)
                .MaximumLength(130)
                .WithMessage("Maximum lenght for note field");

            RuleFor(a => a.OperationType)
                .NotNull()
                .WithMessage("Operation type cannot be null");

            RuleFor(a => a.Amount)
               .GreaterThan(0)
               .WithMessage("Amount must be greather than 0");
        }
    }
}