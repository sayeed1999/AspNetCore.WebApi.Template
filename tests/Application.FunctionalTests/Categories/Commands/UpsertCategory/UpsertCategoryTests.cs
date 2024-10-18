using System;
using AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;
using AspNetCore.WebApi.Template.Application.FunctionalTests.Common;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Categories.Commands.UpsertCategory;

public class UpsertCategoryTests : TestBase
{
    private readonly CreateCategoryCommandHandler _handler;

    public UpsertCategoryTests()
    {
        _handler = new CreateCategoryCommandHandler(_context, _mapper);
    }

    [Test]
    public async Task Handle_ShouldCreateNewCategory_WhenCategoryDoesNotExist()
    {
        // Arrange
        var command = new CreateCategoryCommand
        {
            Id = 0,
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
        var command = new CreateCategoryCommand
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
        var nonExistentCategoryId = 999; // An ID that does not exist
        var command = new CreateCategoryCommand
        {
            Id = nonExistentCategoryId,
            Name = "Non-Existent Category"
        };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Queried object entityInDB was not found, Key: {nonExistentCategoryId}");
    }
}
