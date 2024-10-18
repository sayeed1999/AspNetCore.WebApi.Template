using System.Reflection;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Domain.Entities;
using AspNetCore.WebApi.Template.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Category> Categories => Set<Category>();

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Note:- props validation configuration intentionally not given, as fluent validation pipeline is doing validation already!    
        //builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
