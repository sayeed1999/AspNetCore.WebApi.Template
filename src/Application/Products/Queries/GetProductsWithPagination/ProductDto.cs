using Domain.Entities;

namespace Application.Products.Queries.GetProductsWithPagination;

public class ProductDto
{
    public Guid Id { get; init; }

    public Guid? CategoryId { get; init; }

    public string? Name { get; init; }

    public string? Unit { get; set; }

    public decimal? Price { get; set; }
}
