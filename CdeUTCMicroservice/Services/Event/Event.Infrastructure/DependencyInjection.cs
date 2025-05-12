using Event.Infrastructure.Data.Base;
using Event.Infrastructure.Email;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfraService(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EventDBContext");
            services.AddHttpContextAccessor();
            services.AddDbContext<EventDBContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IEmailService, EmailService>();
            #region Repositories
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            #endregion
            return services;
        }
    }
}
