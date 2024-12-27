using AspNetCore.WebApi.Template.Application.Common.Exceptions;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<ProductDto>
{
    public Guid Id { get; set; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }
}

public class UpdateProductCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IRequestHandler<UpdateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product? entity = await context.Products.FindAsync([request.Id], cancellationToken);

        NotFoundException.ThrowIfNull(entity);

        // partial update by fields...

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            entity!.Name = request.Name;
        }

        if (request.Price != null)
        {
            entity!.Price = request.Price;
        }

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductDto>(entity);
    }
}
