﻿using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data;

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
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger,
        ApplicationDbContext context, UserManager<ApplicationUser> userManager,
        RoleManager<ApplicationRole> roleManager)
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
        ApplicationRole administratorRole = new() { Name = "Administrator" };

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        // Default users
        ApplicationUser administrator =
            new() { UserName = "administrator@localhost", Email = "administrator@localhost" };

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
                    }
                },
                new Category
                {
                    Name = "Pencil",
                    Products =
                    {
                        new Product { Name = "Matador HB Pencil", Price = 7 },
                        new Product { Name = "Matador 2B Pen", Price = 7 }
                    }
                });

            await _context.SaveChangesAsync();
        }
    }
}
