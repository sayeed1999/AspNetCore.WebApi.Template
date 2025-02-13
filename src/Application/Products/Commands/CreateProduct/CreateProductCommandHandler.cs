using System.Data;
using Application.Common.Interfaces;
using Application.Products.Queries.GetProductsWithPagination;
using Domain.Entities;

namespace Application.Products.Commands.CreateProduct;

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
            throw new DuplicateNameException("Product name already exists");
        }

        Product entity = new() { CategoryId = request.CategoryId, Name = request.Name };

        await context.Products.AddAsync(entity, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductDto>(entity);
    }
}
