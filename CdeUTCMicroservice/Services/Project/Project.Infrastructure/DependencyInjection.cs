

using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Data;
using Project.Infrastructure.Data.Interceptors;
using Project.Infrastructure.Data;

namespace Project.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {

            //DI
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            var connectionString = configuration.GetConnectionString("ProjectDBContext");
            services.AddDbContext<ProjectDBContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetService<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<IProjectDBContext, ProjectDBContext>();

            return services;
        }
    }
}
