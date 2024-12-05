using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;

public record UpsertCategoryCommand : IRequest<CategoryDto>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}

public class UpsertCategoryCommandHandler(
    IApplicationDbContext context,
    IMapper mapper)
: IRequestHandler<UpsertCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(UpsertCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? entityInDb = null;

        if (request.Id is null || request.Id == Guid.Empty)
        {
            if (await context.Categories.AnyAsync(
                    c => c.Name == request.Name
                    && c.IsDeleted != true, 
                    cancellationToken))
            {
                throw new ArgumentException("Category with the specified name already exists.", nameof(request.Name));
            }
            
            entityInDb = new Category()
            {
                Name = request.Name,
            };

            await context.Categories.AddAsync(entityInDb, cancellationToken);
        }
        else {
            entityInDb = await context.Categories.SingleOrDefaultAsync(
                        c => c.Id == request.Id && c.IsDeleted != true,
                        cancellationToken);

            if (entityInDb == null)
            {
                throw new ArgumentException("Category with the specified id doesn't exist", nameof(request.Id));
            }
            
            entityInDb.Name = request.Name;
        }
        
        await context.SaveChangesAsync(cancellationToken);

        var dto = mapper.Map<CategoryDto>(entityInDb);
        return dto;
    }
}
