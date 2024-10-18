using AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;
using AspNetCore.WebApi.Template.Application.Products.Commands.UpdateProduct;
using AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Products.Commands;

using static Testing;

public class UpdateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var command = new UpdateProductCommand { Id = 99, Name = "New Name" };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldUpdateProduct()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New List"
        });

        var itemId = await SendAsync(new CreateProductCommand
        {
            CategoryId = listId,
            Name = "New Item"
        });

        var command = new UpdateProductCommand
        {
            Id = itemId,
            Name = "Updated Item Name"
        };

        await SendAsync(command);

        var item = await FindAsync<Product>(itemId);

        item.Should().NotBeNull();
        item!.Name.Should().Be(command.Name);
        item.LastModifiedBy.Should().NotBeNull();
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
