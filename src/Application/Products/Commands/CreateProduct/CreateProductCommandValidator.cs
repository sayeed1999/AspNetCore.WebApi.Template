using Application.Common.Interfaces;

namespace Application.Products.Commands.CreateProduct;

// Note:-
// ASP.NET validation pipeline is not asynchronous and hence can’t invoke asynchronous rules.

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.CategoryId)
            .Must(v => v.HasValue && v.Value != Guid.Empty)
            .When(v => v.CategoryId != null);

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
