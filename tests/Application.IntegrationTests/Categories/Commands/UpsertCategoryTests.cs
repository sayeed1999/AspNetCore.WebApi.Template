using System.Data;
using AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;
using AspNetCore.WebApi.Template.Application.Categories.Queries.GetCategoriesWithPagination;
using AspNetCore.WebApi.Template.Domain.Entities;
using FluentAssertions;
using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Categories.Commands;

public class UpsertCategoryTests : BaseTestFixture
{
    [Test]
    public async Task Create_UniqueName_Success()
    {
        int count = await CountAsync<Category>();
        count.Should().Be(0);

        UpsertCategoryCommand command = new() { Name = "TestCategory - 1" };

        // Act
        CategoryDto categoryDto = await SendAsync(command);

        #region assert

        categoryDto.Should().NotBeNull();
        categoryDto.Id.Should().NotBeEmpty();
        categoryDto.Id.Should().NotBe(Guid.Empty);
        categoryDto.Name.Should().Be("TestCategory - 1");

        #endregion

        count = await CountAsync<Category>();
        count.Should().Be(1);

        command = new UpsertCategoryCommand { Name = "TestCategory - 2" };

        // Act
        categoryDto = await SendAsync(command);

        #region assert

        categoryDto.Should().NotBeNull();
        categoryDto.Id.Should().NotBeEmpty();
        categoryDto.Id.Should().NotBe(Guid.Empty);
        categoryDto.Name.Should().Be("TestCategory - 2");

        #endregion

        count = await CountAsync<Category>();
        count.Should().Be(2);
    }

    [Test]
    public async Task Create_DuplicateName_Failure()
    {
        int count = await CountAsync<Category>();
        count.Should().Be(0);

        UpsertCategoryCommand command = new() { Name = "TestCategory - 1" };

        Func<Task<CategoryDto>> action = () => SendAsync(command);

        // Act
        await action();

        count = await CountAsync<Category>();
        count.Should().Be(1);

        // Act: duplicate entry
        await FluentActions.Invoking(action)
            .Should()
            .ThrowAsync<DuplicateNameException>()
            .WithMessage("Category name is already taken.");
    }
}
