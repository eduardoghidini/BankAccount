using BankAccount.Warren.Domain.Validation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BankAccount.Warren.Application
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddApplicationCQRS(this IServiceCollection services)
        {
            var assembly = AppDomain.CurrentDomain.Load("BankAccount.Warren.Application");
            services.AddMediatR(config => config.AsScoped(), new[]
            {
                assembly
            });
            services.AddScoped<NotificationContext>();
            return services;
        }
    }
}
