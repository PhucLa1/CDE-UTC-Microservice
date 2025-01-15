

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Project.Infrastructure.Data.Extensions
{
    public static class DatabaseExtension
    {
        public static async Task IntialiseDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ProjectDBContext>();

            //context.Database.MigrateAsync().GetAwaiter().GetResult();

            await SeedAsync(context);
        }
        public static async Task SeedAsync(ProjectDBContext context)
        {

        }
    }
}
