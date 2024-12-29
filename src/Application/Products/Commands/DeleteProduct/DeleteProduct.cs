using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Products.Queries.GetProductsWithPagination;
using Domain.Entities;

namespace Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<ProductDto>;

// Note:- Soft delete is safer for real business.
// Hard delete should only be done with background job after a specific period of time.
public class DeleteProductCommandHandler(
    IApplicationDbContext context,
    IMapper mapper
) : IRequestHandler<DeleteProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        Product? entity = await context.Products.FindAsync([request.Id], cancellationToken);

        NotFoundException.ThrowIfNull(entity);

        entity!.IsDeleted = true;

        await context.SaveChangesAsync(cancellationToken);

        return mapper.Map<ProductDto>(entity);
    }
}
