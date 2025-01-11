using Domain.Entities;

namespace Infrastructure.Data.Repositories.Interfaces;

/// <summary>
///     OData endpoints need repository with IQueryable returns for getting entities.
/// </summary>
public interface ICategoryRepository
{
    /// <summary>
    ///     OData will use this IQueryable to perform queries.
    /// </summary>
    /// <returns></returns>
    IQueryable<Category> GetAll();

    /// <summary>
    ///     OData will use this IQueryable to perform queries.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    IQueryable<Category> GetById(Guid id);
}
