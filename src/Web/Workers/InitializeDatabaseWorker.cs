using System;
using AspNetCore.WebApi.Template.Infrastructure.Data;

namespace AspNetCore.WebApi.Template.Web.Workers;

/// <summary>
/// This background service initializes database with auto-migration & seeding data.
/// </summary>
/// <param name="serviceProvider"></param>
public class InitializeDatabaseWorker(IServiceProvider serviceProvider) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using var scope = serviceProvider.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}
