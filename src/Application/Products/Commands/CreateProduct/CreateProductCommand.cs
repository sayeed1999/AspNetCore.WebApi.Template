using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;
using AspNetCore.WebApi.Template.Domain.Events;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<ProductDto>
{
    public Guid? CategoryId { get; init; }
    public string? Name { get; init; }
}
