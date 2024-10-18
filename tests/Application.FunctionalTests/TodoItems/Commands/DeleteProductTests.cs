using AspNetCore.WebApi.Template.Application.Products.Commands.CreateProduct;
using AspNetCore.WebApi.Template.Application.Products.Commands.DeleteProduct;
using AspNetCore.WebApi.Template.Application.Categories.Commands.CreateCategory;
using AspNetCore.WebApi.Template.Domain.Entities;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Products.Commands;

using static Testing;

public class DeleteProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidProductId()
    {
        var command = new DeleteProductCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteProduct()
    {
        var listId = await SendAsync(new CreateCategoryCommand
        {
            Name = "New List"
        });

        var itemId = await SendAsync(new CreateProductCommand
        {
            CategoryId = listId,
            Name = "New Item"
        });

        await SendAsync(new DeleteProductCommand(itemId));

        var item = await FindAsync<Product>(itemId);

        item.Should().BeNull();
    }
}
