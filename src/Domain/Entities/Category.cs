#nullable disable
using System;

namespace AspNetCore.WebApi.Template.Domain.Entities;

public class Category : AuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public byte[] Picture { get; set; }

    public virtual ICollection<Product> Products { get; init; } = new HashSet<Product>();
}
