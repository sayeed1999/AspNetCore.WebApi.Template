using System.Data;
using Application.Categories.Queries.GetCategoriesWithPagination;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Application.Categories.Commands.UpsertCategory;

public class UpsertCategoryCommandHandler(
    IApplicationDbContext context,
    IMapper mapper)
    : IRequestHandler<UpsertCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? entityInDb;

        if (request.Id is null || request.Id == Guid.Empty)
        {
            if (await context.Categories.AnyAsync(
                    c => c.Name == request.Name
                         && c.IsDeleted != true,
                    cancellationToken))
            {
                throw new DuplicateNameException("Category name is already taken.");
            }

            entityInDb = new Category { Name = request.Name };

            await context.Categories.AddAsync(entityInDb, cancellationToken);
        }
        else
        {
            entityInDb = await context.Categories.SingleOrDefaultAsync(
                c => c.Id == request.Id && c.IsDeleted != true,
                cancellationToken);

            NotFoundException.ThrowIfNull(entityInDb);

            entityInDb!.Name = request.Name;
        }

        await context.SaveChangesAsync(cancellationToken);

        CategoryDto? dto = mapper.Map<CategoryDto>(entityInDb);
        return dto;
    }
}
