using BankAccount.Warren.Domain.Users;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BankAccount.Warren.Application.Users.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResult>
    {
        private readonly IUserRepository _userRepository;

        private readonly NotificationContext _notificationContext;

        public LoginCommandHandler(IUserRepository userRepository,
            NotificationContext notificationContext)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _notificationContext = notificationContext ?? throw new ArgumentNullException(nameof(notificationContext));
        }

        public async Task<LoginResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.LoginAsync(request.UserName, request.Password);

            if (user == null)
            {
                _notificationContext.AddNotification("UserNotFount", "User not found");
                return null;
            }
            return new LoginResult()
            {
                AccountId = user.Account.Id,
                UserId = user.Id,
                UserName = user.UserName,
            };
        }
    }
}
