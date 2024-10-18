using AspNetCore.WebApi.Template.Domain.Events;
using Microsoft.Extensions.Logging;

namespace AspNetCore.WebApi.Template.Application.Products.EventHandlers;

public class ProductModifiedEventHandler : INotificationHandler<ProductModifiedEvent>
{
    private readonly ILogger<ProductModifiedEventHandler> _logger;

    public ProductModifiedEventHandler(ILogger<ProductModifiedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(ProductModifiedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation("AspNetCore.WebApi.Template Domain Event: {DomainEvent}", notification.GetType().Name);

        return Task.CompletedTask;
    }
}
