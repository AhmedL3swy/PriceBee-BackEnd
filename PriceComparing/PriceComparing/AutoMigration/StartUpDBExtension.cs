using DataAccess;
using DataAccess.Models;
using EFCore.AutomaticMigrations;
using Microsoft.EntityFrameworkCore;
using PriceComparing.Models;

namespace PriceComparing.AutoMigration
{
	public static class StartUpDBExtension
	{
        public static async Task CreateDbIfNotExists(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<DatabaseContext>();
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                    SeedingData.InitializeDataBase(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");
                }
            }
        }



    }
}
