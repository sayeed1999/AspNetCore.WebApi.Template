using System.Data;
using AspNetCore.WebApi.Template.Application.Categories.Commands.DeleteCategory;
using AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;
using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;
using FluentAssertions;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Categories.Commands;

public class UpsertCategoryTests : BaseTestFixture
{
    private readonly UpsertCategoryCommand _command1 = new() { Name = "Pen" };
    private readonly UpsertCategoryCommand _command2 = new() { Name = "Pencil" };

    private Task<CategoryDto> CreateCategory(UpsertCategoryCommand command)
    {
        return SendAsync(command);
    }

    [Test]
    public async Task Create_UniqueName_Success()
    {
        int count = await CountAsync<Category>();
        count.Should().Be(0);

        // Act
        CategoryDto categoryDto = await CreateCategory(_command1);

        #region assert

        categoryDto.Should().NotBeNull();
        categoryDto.Id.Should().NotBeEmpty();
        categoryDto.Id.Should().NotBe(Guid.Empty);
        categoryDto.Name.Should().Be(_command1.Name);

        #endregion

        count = await CountAsync<Category>();
        count.Should().Be(1);

        // Act
        categoryDto = await CreateCategory(_command2);

        #region assert

        categoryDto.Should().NotBeNull();
        categoryDto.Id.Should().NotBeEmpty();
        categoryDto.Id.Should().NotBe(Guid.Empty);
        categoryDto.Name.Should().Be(_command2.Name);

        #endregion

        count = await CountAsync<Category>();
        count.Should().Be(2);
    }

    [Test]
    public async Task Update_ExistingCategory_Success()
    {
        // Act: initial entry
        CategoryDto categoryDto = await CreateCategory(_command1);

        // Assert: assert initial entry
        Category? itemInDb = await FindAsync<Category>(categoryDto.Id!);
        itemInDb!.Name.Should().Be(_command1.Name);

        // Act: update entry
        UpsertCategoryCommand updateCommand = new() { Id = categoryDto.Id!, Name = "Pen (updated)" };
        await CreateCategory(updateCommand);

        // Assert: assert updated entry
        itemInDb = await FindAsync<Category>(categoryDto.Id!);
        itemInDb!.Name.Should().Be("Pen (updated)");
    }

    [Test]
    public async Task Create_DuplicateName_Failure()
    {
        int count = await CountAsync<Category>();
        count.Should().Be(0);

        // Act: initial entry is done successfully
        await FluentActions.Invoking(() => CreateCategory(_command1))
            .Should()
            .NotThrowAsync();

        // Act: duplicate entry throws exception
        await FluentActions.Invoking(() => CreateCategory(_command1))
            .Should()
            .ThrowAsync<DuplicateNameException>()
            .WithMessage("Category name is already taken.");
    }

    [Test]
    public async Task Create_DuplicateName_Allowed_IfPreviousOneIsSoftDeleted()
    {
        CategoryDto categoryDto = await CreateCategory(_command1);

        DeleteCategoryCommand deleteCommand = new(categoryDto.Id ?? Guid.Empty);
        await SendAsync(deleteCommand);

        await FluentActions.Invoking(() => CreateCategory(_command1))
            .Should()
            .NotThrowAsync();
    }
}
