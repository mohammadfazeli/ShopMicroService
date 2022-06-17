using Microsoft.EntityFrameworkCore;
using Ordering.Infrastracture.Persistence;

namespace Ordering.API.Extentions
{
    public static class MigrationManager
    {
        public static WebApplication SeedMigration(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using (var appContext = scope.ServiceProvider.GetRequiredService<OrderContext>())
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<OrderContextSeed>>();
                    try
                    {
                        logger.LogInformation(" start applay migrations");

                        appContext.Database.Migrate();

                        logger.LogInformation(" finish applay migrations");

                        logger.LogInformation(" start applay seed");
                        OrderContextSeed.SeedAsync(appContext, logger).Wait();
                        logger.LogInformation(" finish applay seed");
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        logger.LogError(ex.Message);
                        throw;
                    }
                }
            }
            return app;
        }
    }
}