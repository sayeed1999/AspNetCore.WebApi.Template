using System;
using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Product, ProductDto>();
    }
}
