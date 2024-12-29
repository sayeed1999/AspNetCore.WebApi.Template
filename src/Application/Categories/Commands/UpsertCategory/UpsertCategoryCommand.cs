using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;

public record UpsertCategoryCommand : IRequest<CategoryDto>
{
    public Guid? Id { get; init; }
    public string? Name { get; init; }
}
