using Price_Comparison.Models;
using EFCore.AutomaticMigrations;

namespace Price_Comparison._ِAutoMigration
{
	public static class StartUpDBExtension
	{
		public static async void CreateDbIfNotExisi(this IHost host)
		{
			using (var scope = host.Services.CreateScope())
			{
				var services = scope.ServiceProvider;
				try
				{
					var context = services.GetRequiredService<ProductComparingDBContext>();
					context.Database.EnsureCreated();
					//MigrateDatabaseToLatestVersion.Execute(context);
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
