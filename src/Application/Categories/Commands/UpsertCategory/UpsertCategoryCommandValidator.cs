using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;

public class UpsertCategoryCommandValidator : AbstractValidator<UpsertCategoryCommand>
{
    private readonly IApplicationDbContext _context;

    public UpsertCategoryCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .NotEmpty()
            .MaximumLength(200);
    }
}
