using System;
using Application.Categories.Queries.GetCategoriesWithPagination;
using Application.Products.Queries.GetProductsWithPagination;
using Domain.Entities;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<Product, ProductDto>();
    }
}
