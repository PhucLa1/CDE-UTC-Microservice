using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Auth.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices
            (this IServiceCollection services, IConfiguration configuration)
        {

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddCookie(x =>
                {
                    x.Cookie.Name = "token";
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidAudience = configuration["Jwt:Audience"],
                        ValidIssuer = configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]!))
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["X-Access-Token"];
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddExceptionHandler<CustomExceptionHandler>();


            return services;
        }

        public static WebApplication UsePresentationServices(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler(options =>
            {

            });
            return webApplication;
        }
    }
}
