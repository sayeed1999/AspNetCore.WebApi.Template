using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Web.Workers;

/// <summary>
/// This recurring background job runs every day once and deletes all entities
/// from database than are in trash for 30days. This worker service helps to clean off
/// soft deleted items from trash after a safe period of time.
/// </summary>
/// <param name="serviceProvider"></param>
/// <param name="logger"></param>
public class PeriodicTrashCleaner(
    IServiceProvider serviceProvider,
    ILogger<PeriodicTrashCleaner> logger) : BackgroundService
{
    private readonly TimeSpan _period = TimeSpan.FromDays(1);
    public bool IsEnabled { get; set; } = true;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var currDate = DateTime.UtcNow;

        using PeriodicTimer timer = new PeriodicTimer(_period);

        while (!stoppingToken.IsCancellationRequested &&
            await timer.WaitForNextTickAsync(stoppingToken))
        {
            try
            {
                if (!IsEnabled)
                {
                    logger.LogInformation($"{nameof(PeriodicTrashCleaner)}  execution skipped..");
                    continue;
                }

                using var scope = serviceProvider.CreateScope();

                var dbContext = scope.ServiceProvider.GetRequiredService<IApplicationDbContext>();

                // bulk delete products from trash older than 30 days
                var deletedProductsCount = await dbContext.Products
                    .Where(x => x.IsDeleted == true && x.LastModified <= currDate.AddDays(-30))
                    .ExecuteDeleteAsync();

                // bulk delete categories from trash older than 30 days
                var deletedCategoriesCount = await dbContext.Categories
                    .Where(x => x.IsDeleted == true && x.LastModified <= currDate.AddDays(-30))
                    .ExecuteDeleteAsync();

                await dbContext.SaveChangesAsync(stoppingToken);

                logger.LogInformation($"{deletedProductsCount} products deleted from trash...");
                logger.LogInformation($"{deletedCategoriesCount} categories deleted from trash...");

            }
            catch (Exception ex)
            {
                logger.LogCritical(ex, $"{nameof(PeriodicTrashCleaner)} crashed while bulk deletion of entities from trash.");
            }
        }
    }
}
