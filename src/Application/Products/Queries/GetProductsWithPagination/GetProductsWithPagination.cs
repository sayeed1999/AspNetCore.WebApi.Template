using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public Guid? CategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string Name { get; init; } = "";
}

public class GetProductsWithPaginationQueryHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Product> query = context.Products.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(c => c.Name.ToLower().Contains(request.Name.Trim().ToLower()));
        }

        if (request.CategoryId != null)
        {
            query = query.Where(x => x.CategoryId == request.CategoryId);
        }

        return await query
            .Where(x => x.IsDeleted != true)
            .OrderBy(x => x.Name)
            .ProjectTo<ProductDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
