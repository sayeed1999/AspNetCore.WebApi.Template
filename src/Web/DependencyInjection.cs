using System.Text.Json.Serialization;
using Application.Common.Interfaces;
using Azure.Identity;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using Web.Extensions;
using Web.Infrastructure;
using Web.Services;
using Web.Workers;

namespace Web;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            })
            .AddOData(options => options
                .AddRouteComponents("odata", GetEdmModel())
                .Select()
                .Filter()
                .OrderBy()
                .SetMaxTop(20)
                .Count()
                .Expand()
            );

        services.AddBackgroundServices();

        services.AddScoped<IUser, CurrentUser>();

        services.AddHttpContextAccessor();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

        // Customise default API behaviour
        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        services.AddOpenApi();
        services.AddOpenApiDocument();
        services.RegisterSwagger(nameof(Web));

        return services;
    }

    private static IEdmModel GetEdmModel()
    {
        ODataConventionModelBuilder builder = new();

        builder.EntitySet<Category>("Categories");
        builder.EntitySet<Product>("Products");

        return builder.GetEdmModel();
    }

    private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<InitializeDatabaseWorker>();

        // Note:- Since we injected the background service in a route, and it's not possible to
        // inject a hosted service through DI, we need to add the service as a Singleton first, 
        // then use it for the hosted service registration.
        services.AddSingleton<PeriodicTrashCleaner>();
        services.AddHostedService(
            provider => provider.GetRequiredService<PeriodicTrashCleaner>());

        return services;
    }

    public static IServiceCollection AddKeyVaultIfConfigured(this IServiceCollection services,
        ConfigurationManager configuration)
    {
        string? keyVaultUri = configuration["AZURE_KEY_VAULT_ENDPOINT"];
        if (!string.IsNullOrWhiteSpace(keyVaultUri))
        {
            configuration.AddAzureKeyVault(
                new Uri(keyVaultUri),
                new DefaultAzureCredential());
        }

        return services;
    }
}
