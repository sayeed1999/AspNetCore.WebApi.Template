using Application.Common.Interfaces;

namespace Application.Categories.Commands.UpsertCategory;

public class UpsertCategoryCommandValidator : AbstractValidator<UpsertCategoryCommand>
{
    public UpsertCategoryCommandValidator(IApplicationDbContext context)
    {
        RuleFor(v => v.Id)
            .Must(v => v.HasValue && v.Value != Guid.Empty)
            .When(v => v.Id != null);

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}
