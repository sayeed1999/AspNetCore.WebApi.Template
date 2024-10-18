using System;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace Domain.UnitTests;

public static partial class ConstantData
{
    public static readonly Product Product1 = new Product
    {
        Name = "Smartphone",
        CategoryId = 1, // Assuming it's linked to the "Electronics" category
        Unit = "Pcs",
        Price = 299.99M,
        ReorderLevel = 10,
        Discontinued = false,
    };

    public static readonly Product Product2 = new Product
    {
        Name = "Apple",
        CategoryId = 2, // Assuming it's linked to the "Groceries" category
        Unit = "Kg",
        Price = 2.99M,
        ReorderLevel = 50,
        Discontinued = false,
    };

    public static List<Product> GenerateMockProducts(int count, Category? category = null)
    {
        var products = new List<Product>();
        for (int i = 1; i <= count; i++)
        {
            products.Add(new Product
            {
                Name = $"Product {i}",
                CategoryId = category?.GetHashCode(), // Assuming category might be null
                Unit = "Pcs",
                Price = i * 10M, // Mock price generation
                ReorderLevel = (short)(i * 5),
                Discontinued = false,
                Category = category
            });
        }
        return products;
    }
}
