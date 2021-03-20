using BankAccount.Warren.Domain.AccountOperations;
using FluentValidation;
using System;

namespace BankAccount.Warren.Api.Models.OperationRequests.Requests
{
    /// <summary>
    /// Registra requisição de execução de operação
    /// </summary>
    public class PostOperationRequest
    {
        /// <summary>
        /// Valor da operação.
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Tipo de operação (Deposit, Payment ou Withdrawn)
        /// </summary>
        public OperationType Type { get; set; }

        /// <summary>
        /// Comentário a reseito da operação.
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Data para executar a operação.
        /// </summary>
        public DateTime OperationDate { get; set; }

        public class PostOperationRequestValidator : AbstractValidator<PostOperationRequest>
        {
            public PostOperationRequestValidator()
            {
                RuleFor(x => x.Amount)
                    .GreaterThan(0)
                        .WithMessage("Amount must be greater than 0");

                RuleFor(x => x.Note)
                    .MaximumLength(130)
                        .WithMessage("Note has a maximum length of 130 chars.");

                RuleFor(x => x.OperationDate.Date)
                    .GreaterThanOrEqualTo(DateTime.Now.Date)
                        .WithMessage("Operation date must be greather or equal than today.");
            }
        }
    }
}