using System.Text.Json.Serialization;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Infrastructure.Data;
using AspNetCore.WebApi.Template.Web.Extensions;
using AspNetCore.WebApi.Template.Web.Infrastructure;
using AspNetCore.WebApi.Template.Web.Services;
using AspNetCore.WebApi.Template.Web.Workers;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.WebApi.Template;

public static class DependencyInjection
{
    public static IServiceCollection AddWebServices(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            });

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
        services.RegisterSwagger(nameof(Template));

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
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
