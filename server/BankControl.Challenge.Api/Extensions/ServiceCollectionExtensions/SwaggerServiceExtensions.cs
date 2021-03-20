using BankAccount.Warren.Api.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace BankAccount.Warren.Api.Extensions.ServiceCollectionExtensions
{

    public static class SwaggerServiceExtensions
    {
        
        public static void AddSwagger(this IServiceCollection services, string title, string version)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = title,
                    Version = version,
                });
                options.DocumentFilter<ServerUrlDocumentFilter>();
                options.IncludeXmlCommentsFromAssembly(Assembly.GetEntryAssembly());
                options.IncludeXmlCommentsFromAssembly(Assembly.GetExecutingAssembly());
            });
           
        }
        public static void UseSwagger(this IApplicationBuilder app, string title, string version, string serviceUrl)
        {
            app.UseSwagger(setup =>
            {
                setup.RouteTemplate = $"/{serviceUrl}/{{documentName}}/swagger.json";
            });

            app.UseSwaggerUI(setup =>
            {
                setup.DocumentTitle = title;
                setup.SwaggerEndpoint($"/{serviceUrl}/{version}/swagger.json", title);
                setup.RoutePrefix = serviceUrl;
            });
        }

        private static void IncludeXmlCommentsFromAssembly(this SwaggerGenOptions options, Assembly assembly)
        {
            var fileName = $"{assembly.GetName().Name}.xml";
            var path = Path.Combine(AppContext.BaseDirectory, fileName);

            if (!File.Exists(path))
            {
                return;
            }

            options.IncludeXmlComments(path, true);
        }
    }
}
