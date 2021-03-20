using BankAccount.Warren.Api.Extensions.ServiceCollectionExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using BankAccount.Warren.Api.Extensions.MvcExtensions;
using BankAccount.Warren.Api.Filters;
using BankAccount.Warren.Api.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using BankAccount.Warren.Database;
using BankAccount.Warren.Application;
using BankAccount.Warren.HangfireMySqlJob;
using System.Text.Json.Serialization;
using BankAccount.Warren.Api.Configurations;
using BankAccount.Warren.Application.Configurations;
using Microsoft.Extensions.Options;

namespace BankAccount.Warren.Api
{
    public class Startup
    {
        private const string ServiceTitle = "Warren - Bank Challenge";
        private const string ServiceVersion = "v1";
        private const string ServiceUrl = "bank-account";


        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IncomeConfiguration>(Configuration.GetSection("Income"));
            services.AddSingleton<IIncomeConfiguration>(f => f.GetRequiredService<IOptions<IncomeConfiguration>>().Value);

            services.AddHttpContextAccessor();
            services
                .AddControllers(options =>
                {
                    options.UseGeneralRoutePrefix($"{ServiceUrl}/v{{version:apiVersion}}");
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });


            var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.Events = new JwtBearerEvents()
               {
                   OnAuthenticationFailed = context =>
                   {
                       return Task.CompletedTask;
                   }
               };
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(key),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

            services.AddMvc(options => options.Filters.Add<NotificationFilter>());

            services.AddFluentValidation();

            services.AddMvcCore()
                .RegisterValidatorsFromAssembly();

            services.AddDatabase(Configuration.GetConnectionString("BankChallenge"));

            services.AddRepositories();

            services.AddApplicationCQRS();

            services.AddHangFireJobClient(Configuration.GetConnectionString("HangFire"));

            services.AddSwagger(ServiceTitle, ServiceVersion);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseCors(policies =>
            {
                policies.AllowAnyOrigin();
                policies.AllowAnyHeader();
                policies.AllowAnyMethod();
            });

            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseApiVersioning();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints
                .MapControllers()
                .RequireAuthorization();
            });

            app.UseSwagger(ServiceTitle, ServiceVersion, ServiceUrl);
        }
    }
}
