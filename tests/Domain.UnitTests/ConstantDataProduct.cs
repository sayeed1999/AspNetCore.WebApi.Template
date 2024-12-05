using System;
using System.Collections.Generic;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace Domain.UnitTests;

public static partial class ConstantData
{
    public static readonly Guid TestProduct1Id = Guid.NewGuid();
    public static readonly Guid TestProduct2Id = Guid.NewGuid();

    public static readonly Product TestProduct1 = MakeProduct(
        id: TestProduct1Id,
        name: "Smartphone",
        categoryId: TestCategory1Id,
        unit: "Pcs",
        price: 299.99M,
        reorderLevel: 10,
        discontinued: false
    );

    public static readonly Product TestProduct2 = MakeProduct(
        id: TestProduct2Id,
        name: "Apple",
        categoryId: TestCategory2Id,
        unit: "Kg",
        price: 2.99M,
        reorderLevel: 50,
        discontinued: false
    );

    public static Product MakeProduct(
        Guid? id = null,
        string? name = null,
        Guid? categoryId = null,
        string? unit = null,
        decimal? price = null,
        short? reorderLevel = null,
        bool? discontinued = null) =>
        new Product
        {
            Id = id ?? Guid.NewGuid(),
            Name = name ?? "Default Product",
            CategoryId = categoryId ?? Guid.NewGuid(),
            Unit = unit ?? "Default Unit",
            Price = price ?? 0.0M,
            ReorderLevel = reorderLevel ?? 0,
            Discontinued = discontinued ?? false,
        };
}
