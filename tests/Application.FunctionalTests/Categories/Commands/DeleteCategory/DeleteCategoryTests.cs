using AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;
using AspNetCore.WebApi.Template.Application.FunctionalTests.Common;
using Microsoft.EntityFrameworkCore;
using static Domain.UnitTests.ConstantData;

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
        await _context.Categories.AddAsync(TestCategory1);
        await _context.SaveChangesAsync();
        
        // Assert Before Act
        var categoryId = _context.Categories.First().Id;
        var command = new DeleteCategoryCommand(categoryId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert After Act
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
        var nonExistentId = Guid.NewGuid(); // Any ID that doesn't exist in mock in-memory database
        var command = new DeleteCategoryCommand(nonExistentId);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>() 
            .WithMessage($"Category with id {nonExistentId} not found (Parameter 'Id')");
    }
}
