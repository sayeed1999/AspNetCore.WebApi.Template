using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<CategoryDto>
{
    public int Id { get; set; } = 0;
    public string? Name { get; init; }
}

public class CreateCategoryCommandHandler(
    IApplicationDbContext _context,
    IMapper _mapper)
: IRequestHandler<CreateCategoryCommand, CategoryDto>
{
    public async Task<CategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        Category? entityInDB = null;

        if (request.Id > 0)
        {
            entityInDB = await _context.Categories.FirstOrDefaultAsync(
                        c => c.Id == request.Id && c.IsDeleted != true,
                        cancellationToken);

            Guard.Against.NotFound(request.Id, entityInDB);
        }

        if (entityInDB is null)
        {
            entityInDB = new Category()
            {
                Name = request.Name,
            };

            await _context.Categories.AddAsync(entityInDB);
        }
        else
        {
            entityInDB.Name = request.Name;
        }

        await _context.SaveChangesAsync(cancellationToken);

        var dto = _mapper.Map<CategoryDto>(entityInDB);
        return dto;
    }
}
