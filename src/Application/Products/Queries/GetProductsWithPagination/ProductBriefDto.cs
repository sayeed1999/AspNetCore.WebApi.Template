using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Products.Queries.GetProductsWithPagination;

public class ProductBriefDto
{
    public int Id { get; init; }

    public int CategoryId { get; init; }

    public string? Name { get; init; }

    public bool Done { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Product, ProductBriefDto>();
        }
    }
}
