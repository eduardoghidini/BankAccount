using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BankAccount.Warren.Api.Filters
{
    public class ServerUrlDocumentFilter : IDocumentFilter
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ServerUrlDocumentFilter(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public void Apply(OpenApiDocument documentation, DocumentFilterContext context)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var scheme = request.Scheme;

            documentation.Servers.Add(new OpenApiServer
            {
                Url = $"{scheme}://{request.Host}",
            });
        }
    }
}
