using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;
using AspNetCore.WebApi.Template.Domain.Events;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;

public record CreateProductCommand : IRequest<ProductDto>
{
    public int? CategoryId { get; init; }

    public string? Name { get; init; }
}

public class CreateProductCommandHandler(
    IApplicationDbContext _context,
    IMapper _mapper
) : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = new Product
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
        };

        await _context.Products.AddAsync(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }
}
