using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data.Interceptors;

public static class DependencyInjection
{
    public static IServiceCollection AddInterceptors(this IServiceCollection services, string connectionString)
    {
        services
            .AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>()
            .AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            // options.UseSqlServer(connectionString);
            options.UseNpgsql(connectionString);
        });

        return services;
    }
}
