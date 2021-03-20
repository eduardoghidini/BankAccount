using BankAccount.Warren.Api.Filters;
using BankAccount.Warren.Api.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BankAccount.Warren.Api.Extensions.ServiceCollectionExtensions
{
    public static class FluentValidationServiceExtensions
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add<ModelValidationFilter>();
            });
            services.AddScoped<IValidatorInterceptor, FluentValidationCheckerMiddleware>();
        }

        public static void RegisterValidatorsFromAssembly(this IMvcCoreBuilder builder)
        {
            builder.AddFluentValidation(validation =>
            {
                validation.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                validation.ImplicitlyValidateChildProperties = true;
                validation.RegisterValidatorsFromAssembly(typeof(Startup).Assembly);
            });
        }
    }
}
