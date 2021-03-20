using BankAccount.Warren.Api.Models.Auth;
using BankAccount.Warren.Api.Models.Auth.Response;
using BankAccount.Warren.Api.Security.Jwt;
using BankAccount.Warren.Application.Users.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankAccount.Warren.Api.Controllers
{
    [ApiVersion("1")]
    [Route("auth")]
    [AllowAnonymous]
    public class AuthorizationController : ApiControllerBase
    {
        public AuthorizationController(IMediator mediator)
            : base(mediator)
        { }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Authenticate([FromBody] LoginRequest request)
        {
            var loginResult = await _mediator.Send(new LoginCommand()
            {
                Password = request.Password,
                UserName = request.UserName
            });

            if (loginResult == null)
            {
                return null;
            }

            var token = TokenService.GenerateToken(loginResult.UserId, loginResult.UserName, loginResult.AccountId);

            return Ok(new LoginResponse()
            {
                Token = token,
                UserName = loginResult.UserName,
            });
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> MeAsync()
        {
            return Ok();
        }

    }
}
