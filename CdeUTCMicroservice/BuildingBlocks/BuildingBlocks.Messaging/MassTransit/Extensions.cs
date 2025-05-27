using BuildingBlocks.Middleware;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Messaging.MassTransit
{
    public static class Extensions
    {
        public static IServiceCollection AddMessageBroker
            (this IServiceCollection services, IConfiguration configuration, Assembly? assembly = null)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<ConfigureUserIdSendObserver>();
            services.AddMassTransit(config =>
            {
                config.SetKebabCaseEndpointNameFormatter();

                if(assembly != null)
                    config.AddConsumers(assembly);

                config.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri(configuration["MessageBroker:Host"]!), host =>
                    {
                        host.Username(configuration["MessageBroker:UserName"]!);
                        host.Password(configuration["MessageBroker:Password"]!);
                    });
                    configurator.ConfigureEndpoints(context);
                    // Đăng ký middleware toàn cục
                    var observer = context.GetRequiredService<ConfigureUserIdSendObserver>();
                    configurator.ConnectSendObserver(observer);
                    configurator.ConnectPublishObserver(observer);
                });

            });
            return services;
        }
    }
}
