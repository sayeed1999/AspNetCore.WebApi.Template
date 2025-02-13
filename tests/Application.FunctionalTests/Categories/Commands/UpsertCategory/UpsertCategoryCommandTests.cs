using Application.Categories.Commands.UpsertCategory;
using Application.Categories.Queries.GetCategoriesWithPagination;
using Application.Common.Exceptions;
using Application.FunctionalTests.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.FunctionalTests.Categories.Commands.UpsertCategory;

public class UpsertCategoryCommandTests : TestBase
{
    private readonly UpsertCategoryCommandHandler _handler;

    public UpsertCategoryCommandTests()
    {
        _handler = new UpsertCategoryCommandHandler(_context, _mapper);
    }

    [Test]
    public async Task Handle_ShouldCreateNewCategory_WhenCategoryDoesNotExist()
    {
        // Arrange
        UpsertCategoryCommand command = new() { Id = Guid.Empty, Name = "New Category" };

        // Act
        CategoryDto result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Category? createdCategory = await _context.Categories
            .FirstOrDefaultAsync(c => c.Name == "New Category");

        createdCategory.Should().NotBeNull();
        result.Name.Should().Be("New Category");
    }

    [Test]
    public async Task Handle_ShouldUpdateCategory_WhenCategoryExists()
    {
        // Arrange
        Category existingCategory = _context.Categories.First();
        UpsertCategoryCommand command = new() { Id = existingCategory.Id, Name = "Updated Category" };

        // Act
        CategoryDto result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Category? updatedCategory = await _context.Categories
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
        Guid nonExistentCategoryId = Guid.NewGuid(); // An ID that does not exist
        UpsertCategoryCommand command = new() { Id = nonExistentCategoryId, Name = "Non-Existent Category" };

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Item with the specified id is not found.");
    }
}
