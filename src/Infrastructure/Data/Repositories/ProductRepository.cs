using Domain.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class ProductRepository(ApplicationDbContext context) : IProductRepository
{
    public IQueryable<Product> GetAll()
    {
        return context.Products.Include(x => x.Category);
    }

    public IQueryable<Product> GetById(Guid id)
    {
        return context.Products.Where(x => x.Id == id).Include(x => x.Category);
    }
}
