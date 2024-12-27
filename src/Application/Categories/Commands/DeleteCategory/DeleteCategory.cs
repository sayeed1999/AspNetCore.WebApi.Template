using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Exceptions;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(Guid Id) : IRequest<CategoryDto>;

public class DeleteCategoryCommandHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IRequestHandler<DeleteCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? entity = await context.Categories
            .Where(l => l.Id == request.Id && l.IsDeleted != true)
            .SingleOrDefaultAsync(cancellationToken);

        NotFoundException.ThrowIfNull(entity);

        entity!.IsDeleted = true; // Soft deletion

        await context.SaveChangesAsync(cancellationToken);

        CategoryDto? dto = mapper.Map<CategoryDto>(entity);
        return dto;
    }
}
