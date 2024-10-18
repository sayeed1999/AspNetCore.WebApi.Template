using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;

public record DeleteCategoryCommand(int Id) : IRequest;

public class DeleteCategoryCommandHandler(IApplicationDbContext _context)
: IRequestHandler<DeleteCategoryCommand>
{
    public async Task Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Categories
            .Where(l => l.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.IsDeleted = true; // Soft deletion
        // _context.Categories.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }
}
