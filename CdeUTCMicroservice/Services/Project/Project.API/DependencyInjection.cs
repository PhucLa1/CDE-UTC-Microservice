using BuildingBlocks.Exceptions.Handler;
using FluentValidation;
using Project.API.Grpc;
using Project.Application.Grpc;

namespace Project.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddExceptionHandler<CustomExceptionHandler>();
            services.AddTransient<IUserGrpc, UserGrpc>();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication webApplication)
        {
            
            webApplication.UseExceptionHandler(options =>
            {

            });
            webApplication.UseStaticFiles();

            return webApplication;
        }
    }
}
