using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using AspNetCore.WebApi.Template.Domain.Events;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<ProductDto>;

public class DeleteProductCommandHandler(
    IApplicationDbContext _context,
    IMapper _mapper
) : IRequestHandler<DeleteProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Products.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }

}
