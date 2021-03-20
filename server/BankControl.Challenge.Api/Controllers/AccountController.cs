using BankAccount.Warren.Api.Models.Accounts.Response;
using BankAccount.Warren.Application.Accounts.GetAccount;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BankAccount.Warren.Api.Controllers
{
    [ApiVersion("1")]
    [Route("account")]
    public class AccountController : ApiControllerBase
    {
        public AccountController(IMediator mediator)
            : base(mediator)
        { }

        [HttpGet]
        [Route("me")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetAccountResponse))]
        public async Task<IActionResult> MeAsync()
        {
            var result = await _mediator.Send(new GetAccountQuery()
            {
                AccountId = AccountId
            });
            return Ok(new GetAccountResponse()
            {
                AccountNumber = result.AccountNumber,
                CurrentAmount = result.CurrentAmount,
                Name = result.Name

            });
        }
    }
}
