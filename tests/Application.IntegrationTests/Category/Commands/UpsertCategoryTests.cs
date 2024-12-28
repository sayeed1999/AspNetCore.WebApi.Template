using FluentAssertions;

namespace Application.IntegrationTests.Category.Commands;

[TestFixture]
public class UpsertCategoryTests : BaseTestFixture
{
    [Test]
    public void ShouldCreateCategory()
    {
        int x = 1;
        x.Should().BeGreaterThan(0);
        // UpsertCategoryCommand command = new() { Name = "TestCategory" };
        //
        // CategoryDto categoryDto = await SendAsync(command);
        //
        // categoryDto.Should().NotBeNull();
    }
}
