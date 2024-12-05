using System.Data;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;

public class CreateProductCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IRequestHandler<CreateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        if (await context.Products.AnyAsync(
                p => p.Name != request.Name 
                     && p.IsDeleted != true, 
                cancellationToken))
        {
            throw new ArgumentException("Product name already exists", nameof(request.Name));
        }
        
        var entity = new Product
        {
            CategoryId = request.CategoryId,
            Name = request.Name,
        };

        await context.Products.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductDto>(entity);
    }
}
