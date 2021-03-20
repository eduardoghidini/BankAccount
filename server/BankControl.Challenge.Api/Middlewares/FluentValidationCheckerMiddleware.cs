using BankAccount.Warren.Domain.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace BankAccount.Warren.Api.Middlewares
{
    public class FluentValidationCheckerMiddleware : IValidatorInterceptor
    {
        private readonly NotificationContext _context;

        public FluentValidationCheckerMiddleware(NotificationContext context)
        {
            _context = context;
        }

        public ValidationResult AfterMvcValidation(ControllerContext controllerContext, IValidationContext commonContext, ValidationResult result)
        {
            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    _context.AddNotification(error.PropertyName, error.ErrorMessage);
                }
            }
            return result;
        }

        public IValidationContext BeforeMvcValidation(ControllerContext controllerContext, IValidationContext commonContext)
        {
            return commonContext;
        }
    }
}
