using BankAccount.Warren.Api.Models.Abstractions;
using BankAccount.Warren.Api.Models.Operations;
using BankAccount.Warren.Application.AccountOperations.ListOperations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Warren.Api.Controllers
{
    [ApiVersion("1")]
    [Route("operation")]
    public class OperationsContorller : ApiControllerBase
    {
        public OperationsContorller(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<OperationResponse>))]
        public async Task<IActionResult> ListOperationsAsync([FromQuery] int pageSize, [FromQuery] int page)
        {
            var result = await _mediator.Send(new ListOperationQuery()
            {
                AccountId = AccountId,
                PageSize = pageSize,
                PageNumber = page
            });

            return Ok(new PagedResponse<OperationResponse>()
            {
                Data = result.Data.Select(_ => new OperationResponse()
                {
                    Amount = _.Amount,
                    Note = _.Note,
                    Type = _.Type,
                    Date = _.OperationDate
                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalPages
            });
        }
    }
}
