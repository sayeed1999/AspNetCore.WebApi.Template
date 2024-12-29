using Application.Categories.Queries.GetCategoriesWithPagination;

namespace Application.Categories.Commands.UpsertCategory;

public record UpsertCategoryCommand : IRequest<CategoryDto>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}
