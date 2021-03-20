using FluentValidation;

namespace BankAccount.Warren.Api.Models.Auth
{
    public class LoginRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public class LoginRequestValidator : AbstractValidator<LoginRequest>
        {
            public LoginRequestValidator()
            {
                RuleFor(x => x.UserName)
                    .NotEmpty()
                        .WithMessage("Username must be filled");
                    
                RuleFor(x => x.Password)
                    .NotEmpty()
                        .WithMessage("Password must be filled");
            }
        }
    }
}
