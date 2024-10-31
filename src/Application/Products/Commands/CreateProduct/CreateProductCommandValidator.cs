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
            .Must(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    // ASP.NET validation pipeline is not asynchronous and hence can’t invoke asynchronous rules.
    // Ref: https://medium.com/cheranga/using-asynchronous-fluent-validations-in-asp-net-api-831710b0b9cd
    public bool BeUniqueTitle(string title) =>
        _context.Categories.All(l => l.Name != title && l.IsDeleted != true);
}
