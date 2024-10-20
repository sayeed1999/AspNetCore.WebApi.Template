using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.UpdateProduct;

public record UpdateProductCommand : IRequest<ProductDto>
{
    public int Id { get; set; }
    public string? Name { get; init; }
    public decimal? Price { get; init; }

}

public class UpdateProductCommandHandler(
    IApplicationDbContext _context,
    IMapper _mapper
) : IRequestHandler<UpdateProductCommand, ProductDto>
{
    public async Task<ProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        // partial update by fields!
        if (!string.IsNullOrWhiteSpace(request.Name)) entity.Name = request.Name;
        if (request.Price != null) entity.Price = request.Price;

        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<ProductDto>(entity);
    }
}
