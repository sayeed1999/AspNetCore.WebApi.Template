using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;

public class ProductDto
{
    public int Id { get; init; }

    public int CategoryId { get; init; }

    public string? Name { get; init; }

    public string? Unit { get; set; }

    public decimal? Price { get; set; }
}
