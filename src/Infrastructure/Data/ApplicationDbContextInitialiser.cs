using System.Runtime.InteropServices;
using AspNetCore.WebApi.Template.Domain.Constants;
using AspNetCore.WebApi.Template.Domain.Entities;
using AspNetCore.WebApi.Template.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi.Template.Infrastructure.Data;

// public static class InitialiserExtensions
// {
//     public static async Task InitialiseDatabaseAsync(this WebApplication app)
//     {
//         using var scope = app.Services.CreateScope();
//         var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
//         await initialiser.InitialiseAsync();s
//         await initialiser.SeedAsync();
//     }
// }

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(Roles.Administrator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost" };

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
            }
        }

        // Default data
        // Seed, if necessary
        if (!_context.Categories.Any())
        {
            await _context.Categories.AddRangeAsync(
                new Category
                {
                    Name = "Pen",
                    Products =
                {
                    new Product { Name = "Matador Ball Pen", Price = 5 },
                    new Product { Name = "Matador Gel Pen", Price = 6 }
                },
                },
                new Category
                {
                    Name = "Pencil",
                    Products =
                {
                    new Product { Name = "Matador HB Pencil", Price = 7 },
                    new Product { Name = "Matador 2B Pen", Price = 7 }
                },
                });

            await _context.SaveChangesAsync();
        }
    }
}
