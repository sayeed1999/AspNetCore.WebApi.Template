using Application.Common.Interfaces;
using Application.Products.Queries.GetProductsWithPagination;
using Domain.Entities;
using Domain.Events;

namespace Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<ProductDto>
{
    public Guid? CategoryId { get; init; }
    public string? Name { get; init; }
}
