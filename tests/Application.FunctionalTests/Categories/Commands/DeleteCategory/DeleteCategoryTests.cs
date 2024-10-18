using System;
using AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;
using AspNetCore.WebApi.Template.Application.FunctionalTests.Common;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Categories.Commands.DeleteCategory;

public class DeleteCategoryTests : TestBase
{
    private readonly DeleteCategoryCommandHandler _handler;

    public DeleteCategoryTests()
    {
        _handler = new DeleteCategoryCommandHandler(_context, _mapper);
    }

    [Test]
    public async Task Handle_ShouldSoftDeleteCategory_WhenCategoryExists()
    {
        // Arrange
        var categoryId = _context.Categories.First().Id;
        var command = new DeleteCategoryCommand(categoryId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        var deletedCategory = await _context.Categories
            .Where(c => c.Id == categoryId)
            .SingleOrDefaultAsync();

        deletedCategory!.IsDeleted.Should().BeTrue();
        result.Id.Should().Be(categoryId);
    }

    [Test]
    public async Task Handle_ShouldThrowNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        var nonExistentCategoryId = 999; // Any ID that doesn't exist in mock in-memory database
        var command = new DeleteCategoryCommand(nonExistentCategoryId);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Queried object entity was not found, Key: {nonExistentCategoryId}");
    }
}
