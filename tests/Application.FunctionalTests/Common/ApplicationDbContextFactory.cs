using System;
using AspNetCore.WebApi.Template.Domain.Entities;
using AspNetCore.WebApi.Template.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Common;

public class ApplicationDbContextFactory
{
    public static ApplicationDbContext Create()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                        .UseInMemoryDatabase(Guid.NewGuid().ToString())
                        .Options;

        var context = new ApplicationDbContext(options);

        context.Database.EnsureCreated();

        context.Categories.AddRange(
           new Category
           {
               Name = "Pen",
               Description = "...",
               Products = new List<Product>()
               {
                    new Product { Name = "Matador Ball Pen", Price = 5 },
                    new Product { Name = "Matador Gen Pen", Price = 6 },
               }
           },
           new Category
           {
               Name = "Pencil",
               Description = "...",
               Products = new List<Product>()
               {
                    new Product { Name = "Matador HB Pencil", Price = 10 },
                    new Product { Name = "Matador 2B Pencil", Price = 11 },
               }
           }
       );

        context.SaveChanges();

        return context;
    }

    public static void Destroy(ApplicationDbContext context)
    {
        context.Database.EnsureDeleted();

        context.Dispose();
    }
}
