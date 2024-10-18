using AspNetCore.WebApi.Template.Application.Common.Exceptions;
using AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;
using AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Products.Commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateProductCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New List"
        });

        var command = new CreateProductCommand
        {
            CategoryId = listId,
            Name = "Tasks"
        };

        var itemId = await SendAsync(command);

        var item = await FindAsync<Product>(itemId);

        item.Should().NotBeNull();
        item!.CategoryId.Should().Be(command.CategoryId);
        item.Name.Should().Be(command.Name);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
