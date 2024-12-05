using System;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;

public class CategoryDto
{
    public Guid? Id { get; init; }

    public string? Name { get; init; }
}
