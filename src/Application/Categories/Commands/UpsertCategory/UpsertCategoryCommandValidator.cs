using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200)
            .Must(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    // ASP.NET validation pipeline is not asynchronous and hence can’t invoke asynchronous rules.
    // Ref: https://medium.com/cheranga/using-asynchronous-fluent-validations-in-asp-net-api-831710b0b9cd
    private bool BeUniqueTitle(string title) =>
        _context.Categories.All(l => l.Name != title && l.IsDeleted != true);
}
