using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Categories.Queries.GetCategoriesWithPagination;

public record GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string Name { get; init; } = "";
}

public class GetCategoriesWithPaginationQueryHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryDto>>
{
    public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Category> query = context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(c => c.Name.ToLower().Contains(request.Name.Trim().ToLower()));
        }

        return await query
            .Where(x => x.IsDeleted != true)
            .OrderBy(x => x.Name)
            .ProjectTo<CategoryDto>(mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize, cancellationToken);
    }
}
