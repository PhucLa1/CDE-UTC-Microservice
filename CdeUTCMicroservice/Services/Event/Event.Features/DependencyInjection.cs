using BuildingBlocks.Behaviors;
using BuildingBlocks.Messaging.MassTransit;
using Event.Shared.Setting;
using System.Reflection;

namespace Event.Features
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFeaturesService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSetting>(configuration.GetSection("Email"));

            //Async communication Services
            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
            services.AddHttpContextAccessor();
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }

        public static WebApplication UseFeaturesServices(this WebApplication webApplication)
        {
            webApplication.UseExceptionHandler(options =>
            {

            });
            return webApplication;
        }
    }
}
