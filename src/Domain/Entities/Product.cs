#nullable disable
using System;

namespace AspNetCore.WebApi.Template.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; set; }
    public int? CategoryId { get; set; }
    public string Unit { get; set; } = "Pcs"; // Pcs, Kg, ...
    public decimal? Price { get; set; }
    public short? ReorderLevel { get; set; }
    public bool Discontinued { get; set; }

    public virtual Category Category { get; set; }
}
