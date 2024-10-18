using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Application.Common.Mappings;
using AspNetCore.WebApi.Template.Application.Common.Models;

namespace AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;

public record GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string Name { get; init; } = "";
}

public class GetCategoriesWithPaginationQueryHandler(
    IApplicationDbContext _context,
    IMapper _mapper)
: IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryDto>>
{
    public async Task<PaginatedList<CategoryDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Categories.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
        {
            query = query.Where(c => c.Name.ToLower().Contains(request.Name.Trim().ToLower()));
        }

        return await query
            .Where(x => x.IsDeleted != true)
            .OrderBy(x => x.Name)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
