

namespace Auth.Data.Data.Extensions
{
    public class SeedData
    {
        public async static void InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new AuthDBContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<AuthDBContext>>()))
            {
                await SeedAsync(context);
            }
        }
        public static async Task SeedAsync(AuthDBContext context)
        {
            await SeedCityAsync(context);
            await SeedUserAsync(context);
            await SeedLanguageAsync(context);
            await SeedJobTitleAsync(context);
        }

        private static async Task SeedCityAsync(AuthDBContext context)
        {
        }
        private static async Task SeedUserAsync(AuthDBContext context)
        {

        }
        private static async Task SeedLanguageAsync(AuthDBContext context)
        {

        }
        private static async Task SeedJobTitleAsync(AuthDBContext context)
        {

        }
    }
}
