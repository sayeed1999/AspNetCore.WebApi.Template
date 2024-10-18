using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest<CategoryDto>;

public class DeleteCategoryCommandHandler(
    IApplicationDbContext _context,
    IMapper _mapper)
: IRequestHandler<DeleteCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .Where(l => l.Id == request.Id && l.IsDeleted != true)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.IsDeleted = true; // Soft deletion
        // _context.Categories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<CategoryDto>(entity);
        return dto;
    }
}
