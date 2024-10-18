using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Domain.Events;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(int Id) : IRequest<int>;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
{
    private readonly IApplicationDbContext _context;

    public DeleteProductCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Products
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Products.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

}
