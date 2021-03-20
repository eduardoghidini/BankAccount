using BankAccount.Warren.Api.Security.Jwt;
using BankAccount.Warren.Domain.Validation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net.Mime;

namespace BankAccount.Warren.Api.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Notification[]))]
    public class ApiControllerBase : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected int AccountId
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(_ => _.Type == ClaimsDefinitions.AccountId);
                int accountId = 0;
                if (int.TryParse(claim?.Value, out accountId))
                {
                    return accountId;
                }
                return 0;
            }
        }


        public ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
