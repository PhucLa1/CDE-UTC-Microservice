using Auth.Repositories.Base;
using Auth.Repositories.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryServices
           (this IServiceCollection services, IConfiguration configuration)
        {

            #region Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion


            #region Setting
            services.Configure<JwtSetting>(configuration.GetSection("Jwt"));
            #endregion


            return services;
        }
    }
}
