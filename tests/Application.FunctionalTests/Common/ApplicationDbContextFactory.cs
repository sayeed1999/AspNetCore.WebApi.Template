using AspNetCore.WebApi.Template.Domain.Entities;
using AspNetCore.WebApi.Template.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Common;

public class ApplicationDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        DbContextOptions<ApplicationDbContext> options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // when using in-memory database
            .Options;

        ApplicationDbContext context = new(options);

        context.Database.EnsureCreated();

        SeedData(context);

        return context;
    }

    private static void SeedData(ApplicationDbContext context)
    {
        context.Categories.AddRange(
            new Category
            {
                Name = "Pen",
                Description = "...",
                Products = new List<Product>
                {
                    new() { Name = "Matador Ball Pen", Price = 5 },
                    new() { Name = "Matador Gen Pen", Price = 6 }
                }
            },
            new Category
            {
                Name = "Pencil",
                Description = "...",
                Products = new List<Product>
                {
                    new() { Name = "Matador HB Pencil", Price = 10 },
                    new() { Name = "Matador 2B Pencil", Price = 11 }
                }
            }
        );

        context.SaveChanges();
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}
