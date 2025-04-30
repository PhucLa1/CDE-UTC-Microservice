using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Application.Data;
using Project.Application.Hubs;
using Project.Infrastructure.Data;
using Project.Infrastructure.Data.Base;
using Project.Infrastructure.Hubs;

namespace Project.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {


            var connectionString = configuration.GetConnectionString("ProjectDBContext");
            services.AddSignalR();
            services.AddHttpContextAccessor();
            services.AddDbContext<ProjectDBContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IAnnotationHub, AnnotationHub>();

            return services;
        }
    }
}
