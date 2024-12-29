using System;
using Domain.Entities;

namespace Application.Categories.Queries.GetCategoriesWithPagination;

public class CategoryDto
{
    public Guid? Id { get; init; }

    public string? Name { get; init; }
}
