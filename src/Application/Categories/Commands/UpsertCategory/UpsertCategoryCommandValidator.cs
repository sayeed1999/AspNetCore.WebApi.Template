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
            .MustAsync(BeUniqueTitle)
                .WithMessage("'{PropertyName}' must be unique.")
                .WithErrorCode("Unique");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AllAsync(l => l.Name != title && l.IsDeleted != true, cancellationToken);
    }
}
