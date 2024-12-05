using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest<CategoryDto>;

public class DeleteCategoryCommandHandler(
    IApplicationDbContext context,
    IMapper mapper)
: IRequestHandler<DeleteCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Categories
            .Where(l => l.Id == request.Id && l.IsDeleted != true)
            .SingleOrDefaultAsync(cancellationToken);

        if (entity == null)
        {
            throw new ArgumentException($"Category with id {request.Id} not found", nameof(request.Id));
        }

        Guard.Against.NotFound(request.Id, entity);

        entity.IsDeleted = true; // Soft deletion

        await context.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<CategoryDto>(entity);
        return dto;
    }
}
