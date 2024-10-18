using System;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace Domain.UnitTests;

public static partial class ConstantData
{
    public static readonly Category Category1 = new Category
    {
        Name = "Electronics",
        Description = "Devices, gadgets, and accessories",
        Picture = new byte[0], // Placeholder for picture byte array
    };

    public static readonly Category Category2 = new Category
    {
        Name = "Groceries",
        Description = "Daily essentials and food items",
        Picture = new byte[0],
    };

    public static List<Category> GenerateMockCategories(int count)
    {
        var categories = new List<Category>();
        for (int i = 1; i <= count; i++)
        {
            categories.Add(new Category
            {
                Name = $"Category {i}",
                Description = $"Description for Category {i}",
                Picture = new byte[0], // You can add a default byte array for mock data
            });
        }
        return categories;
    }
}

