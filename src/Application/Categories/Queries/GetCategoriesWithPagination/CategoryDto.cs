using System;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;

public class CategoryDto
{
    public int Id { get; init; }

    public string? Name { get; init; }
}
