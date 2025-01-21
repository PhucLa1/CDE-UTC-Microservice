using Event.Shared.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Shared
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddShareService(this IServiceCollection services, IConfiguration configuration)
        {

            return services;
        }
    }
}
