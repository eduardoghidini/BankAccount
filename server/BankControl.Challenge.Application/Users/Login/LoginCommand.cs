using MediatR;

namespace BankAccount.Warren.Application.Users.Login
{
    public class LoginCommand : IRequest<LoginResult>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
