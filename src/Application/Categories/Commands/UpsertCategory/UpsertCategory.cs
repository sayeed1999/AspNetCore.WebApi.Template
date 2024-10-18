using AspNetCore.WebApi.Template.Application.Common.Interfaces;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;

public record CreateCategoryCommand : IRequest<int>
{
    public string? Name { get; init; }
}

public class CreateCategoryCommandHandler(IApplicationDbContext _context)
: IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var entityInDB = await _context.Categories.FirstOrDefaultAsync(
            c => c.Name == request.Name,
            cancellationToken);

        if (entityInDB is null)
        {

            entityInDB = new Category()
            {
                Name = request.Name,
            };

            await _context.Categories.AddAsync(entityInDB);
        }
        else
        {
            entityInDB.Name = request.Name;
        }

        await _context.SaveChangesAsync(cancellationToken);

        return entityInDB.Id;
    }
}
