﻿using AspNetCore.WebApi.Template.Application.Common.Interfaces;

namespace AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;

// Note:-
// ASP.NET validation pipeline is not asynchronous and hence can’t invoke asynchronous rules.

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateProductCommandValidator(IApplicationDbContext context)
    {
        _context = context;

        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
    }
}
