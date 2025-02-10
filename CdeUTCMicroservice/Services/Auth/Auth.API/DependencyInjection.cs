using Auth.API.ProtosEndpoint;
using BuildingBlocks.Exceptions.Handler;

namespace Auth.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentationServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            services.AddGrpc();
            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
            services.AddHttpContextAccessor();
            

            services.AddExceptionHandler<CustomExceptionHandler>();


            return services;
        }

        public static WebApplication UsePresentationServices(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler(options =>
            {

            });
            webApplication.MapGrpcService<UserProto>();
            return webApplication;
        }
    }
}
