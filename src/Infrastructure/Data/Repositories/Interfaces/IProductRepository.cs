using Domain.Entities;

namespace Infrastructure.Data.Repositories.Interfaces;

/// <summary>
///     OData endpoints need repository with IQueryable returns for getting entities.
/// </summary>
/// <param name="context"></param>
public interface IProductRepository
{
    /// <summary>
    ///     OData will use this IQueryable to perform queries.
    /// </summary>
    /// <returns></returns>
    IQueryable<Product> GetAll();

    /// <summary>
    ///     OData will use this IQueryable to perform queries.
    /// </summary>
    /// <returns></returns>
    IQueryable<Product> GetById(Guid id);
}
