using AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;
using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Application.Common.Exceptions;
using AspNetCore.WebApi.Template.Application.FunctionalTests.Common;
using AspNetCore.WebApi.Template.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Categories.Commands.DeleteCategory;

[TestFixture]
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
        Guid categoryId = _context.Categories.First().Id;
        DeleteCategoryCommand command = new(categoryId);

        // Act
        CategoryDto result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Category? deletedCategory = await _context.Categories
            .Where(c => c.Id == categoryId && c.IsDeleted == true)
            .SingleOrDefaultAsync();

        deletedCategory!.IsDeleted.Should().BeTrue();
        result.Id.Should().Be(categoryId);
    }

    [Test]
    public async Task Handle_ShouldThrowNotFound_WhenCategoryDoesNotExist()
    {
        // Arrange
        Guid nonExistentId = Guid.NewGuid(); // Any ID that doesn't exist in mock in-memory database
        DeleteCategoryCommand command = new(nonExistentId);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Item with the specified id is not found.");
    }
}
