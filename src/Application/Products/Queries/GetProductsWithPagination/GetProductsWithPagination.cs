using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Common.Mappings;
using AspNetCore.WebApi.Template.Application.Common.Models;

namespace AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;

public record GetProductsWithPaginationQuery : IRequest<PaginatedList<ProductDto>>
{
    public int? CategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string Name { get; init; } = "";
}

public class GetProductsWithPaginationQueryHandler(
    IApplicationDbContext _context,
    IMapper _mapper)
: IRequestHandler<GetProductsWithPaginationQuery, PaginatedList<ProductDto>>
{
    public async Task<PaginatedList<ProductDto>> Handle(GetProductsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Products.AsQueryable();

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
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
