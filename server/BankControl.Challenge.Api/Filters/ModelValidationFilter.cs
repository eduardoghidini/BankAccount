using BankAccount.Warren.Domain.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace BankAccount.Warren.Api.Filters
{
    public class ModelValidationFilter : IAsyncActionFilter
    {
        private readonly NotificationContext _notificationContex;
        public ModelValidationFilter()
        {

        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
            else
            {
                await next();
            }
        }
        //private ObjectResult CreateNotificationErrorResult(ModelStateDictionary result)
        //{
        //    var list = new List<Notification>();
        //    foreach (var modelState in result.Values)
        //    {
        //        foreach (ModelError error in modelState.Errors)
        //        {
        //        list.Add("field", )           
        //        }
        //    }
        //}
    }
}
