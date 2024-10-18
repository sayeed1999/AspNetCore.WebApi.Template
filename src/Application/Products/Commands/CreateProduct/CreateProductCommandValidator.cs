using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty()
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AllAsync(l => l.Name != title && l.IsDeleted != true, cancellationToken);
    }
}
