

using Auth.Data.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Event.Infrastructure.Data
{
    public class SeedData
    {
        public async static void InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new EventDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EventDBContext>>()))
            {
                await SeedAsync(context);
            }
        }
        public static async Task SeedAsync(EventDBContext context)
        {
            await context.ActivityTypeParents.AddRangeAsync(InitalData.ActivityTypeParents);
            await context.SaveChangesAsync();
        }  
    }
}
