using Domain.Entities;
using Infrastructure.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Repositories;

public class CategoryRepository(ApplicationDbContext context) : ICategoryRepository
{
    public IQueryable<Category> GetAll()
    {
        return context.Categories.Include(x => x.Products);
    }

    public IQueryable<Category> GetById(Guid id)
    {
        return context.Categories.Where(x => x.Id == id).Include(x => x.Products);
    }
}
