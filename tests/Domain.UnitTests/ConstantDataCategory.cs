using System;
using System.Collections.Generic;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace Domain.UnitTests;

public static partial class ConstantData
{
    public static readonly Guid TestCategory1Id = Guid.NewGuid();
    public static readonly Guid TestCategory2Id = Guid.NewGuid();
    
    public static readonly Category TestCategory1 = MakeCategory(
        id: TestCategory1Id,
        name: "Electronics",
        description: "Devices, gadgets, and accessories"
    );

    public static readonly Category TestCategory2 = MakeCategory(
        id: TestCategory2Id,
        name: "Groceries",
        description: "Daily essentials and food items"
    );

    public static Category MakeCategory(
        Guid? id = null,
        string? name = null,
        string? description = null,
        byte[]? picture = null) =>
        new Category
        {
            Id = id ?? Guid.NewGuid(),
            Name = name ?? "Default Category",
            Description = description ?? "Default Description",
            Picture = picture ?? [],
        };
}
