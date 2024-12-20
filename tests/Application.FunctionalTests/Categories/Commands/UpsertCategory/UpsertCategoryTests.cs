using System;
using AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;
using AspNetCore.WebApi.Template.Application.FunctionalTests.Common;
using AspNetCore.WebApi.Template.Domain.Entities;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Categories.Commands.UpsertCategory;

public class UpsertCategoryTests : TestBase
{
    private readonly UpsertCategoryCommandHandler _handler;

    public UpsertCategoryTests()
    {
        _handler = new UpsertCategoryCommandHandler(_context, _mapper);
    }

    [Test]
    public async Task Handle_ShouldCreateNewCategory_WhenCategoryDoesNotExist()
    {
        // Arrange
        var command = new UpsertCategoryCommand
        {
            Id = Guid.Empty,
            Name = "New Category"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var createdCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == "New Category");

        createdCategory.Should().NotBeNull();
        result.Name.Should().Be("New Category");
    }

    [Test]
    public async Task Handle_ShouldUpdateCategory_WhenCategoryExists()
    {
        // Arrange
        var existingCategory = _context.Categories.First();
        var command = new UpsertCategoryCommand
        {
            Id = existingCategory.Id,
            Name = "Updated Category"
        };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var updatedCategory = await _context.Categories
            .Where(c => c.Id == existingCategory.Id)
            .SingleOrDefaultAsync();

        updatedCategory.Should().NotBeNull();
        updatedCategory!.Name.Should().Be("Updated Category");
        result.Name.Should().Be("Updated Category");
    }

    [Test]
    public async Task Handle_ShouldThrowNotFoundException_WhenCategoryIdDoesNotExist()
    {
        // Arrange
        var nonExistentCategoryId = Guid.NewGuid(); // An ID that does not exist
        var command = new UpsertCategoryCommand
        {
            Id = nonExistentCategoryId,
            Name = "Non-Existent Category"
        };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
            .WithMessage($"{nameof(Category)} with the specified id doesn't exist (Parameter 'Id')");
    }
}
