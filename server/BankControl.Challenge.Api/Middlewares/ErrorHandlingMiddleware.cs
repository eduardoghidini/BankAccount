using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BankAccount.Warren.Domain.Validation;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace BankAccount.Warren.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var notifications = JsonConvert.SerializeObject(new List<Notification>()
            {
                new Notification("GenericError", "Error to process operation")
            });
            //Log.Error("erro", ex);
            await context.Response.WriteAsync(notifications);
        }
    }
}

