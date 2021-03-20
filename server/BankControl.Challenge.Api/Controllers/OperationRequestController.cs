using BankAccount.Warren.Api.Models.Abstractions;
using BankAccount.Warren.Api.Models.OperationRequests.Requests;
using BankAccount.Warren.Api.Models.OperationRequests.Response;
using BankAccount.Warren.Application.AccountOperations.CancelOperationRequest;
using BankAccount.Warren.Application.AccountOperations.GetAccountOperationRequest;
using BankAccount.Warren.Application.AccountOperations.SaveOperationRequest;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BankAccount.Warren.Api.Controllers
{
    [ApiVersion("1")]
    [Route("operation-request")]
    public class OperationRequestController : ApiControllerBase
    {
        public OperationRequestController(IMediator mediator)
            : base(mediator)
        {
        }
        /// <summary>
        /// Registra uma solicitação de execução de operação.
        /// </summary>
        /// <param name="request">Informações sobre a operação.</param>
        /// <returns>Sucesso caso a operação for registrada corretamente.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> PostOperationRequesAsync([FromBody] PostOperationRequest request)
        {
            var result = await _mediator.Send(new SaveOperationRequestCommand()
            {
                AccountId = AccountId,
                Amount = request.Amount,
                Note = request.Note,
                OperationDate = request.OperationDate,
                Type = request.Type,
            });

            return Ok();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOperationRequestAsync([FromRoute] int id)
        {
            var result = await _mediator.Send(new CancelOperationRequestCommand()
            {
                AccountId = AccountId,
                OperationId = id
            });
            return Ok();
        }

        [HttpGet("list")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<OperationRequestResponse>))]
        public async Task<IActionResult> ListOperationsAsync([FromQuery] int pageSize, [FromQuery] int page)
        {
            var result = await _mediator.Send(new GetAccountOperationRequestQuery()
            {
                AccountId = AccountId,
                PageSize = pageSize,
                Page = page
            });

            return Ok(new PagedResponse<OperationRequestResponse>()
            {
                Data = result.Data.Select(_ => new OperationRequestResponse()
                {
                    Amount = _.Amount,
                    Type = _.Type,
                    Id = _.Id,
                    OperationResponseMessage = _.OperationResponseMessage,
                    Status = _.Status,
                    ProcessedDate = _.ProcessedDate,
                    RequestedDate = _.RequestedDate,
                }).ToList(),
                PageNumber = result.PageNumber,
                PageSize = result.PageSize,
                TotalRecords = result.TotalPages
            });
        }
    }
}
