namespace AspNetCore.WebApi.Template.Domain.Events;

public class ProductModifiedEvent : BaseEvent
{
    public ProductModifiedEvent(Product item)
    {
        Item = item;
    }

    public Product Item { get; }
}
