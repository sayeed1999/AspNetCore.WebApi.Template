using AspNetCore.WebApi.Template.Application.Categories.Commands.UpsertCategory;
using FluentValidation.TestHelper;

namespace AspNetCore.WebApi.Template.Application.FunctionalTests.Categories.Commands.UpsertCategory;

public class UpsertCategoryCommandValidatorTests
{
    // Mock IApplicationDbContext if needed; otherwise, pass null or default.
    private readonly UpsertCategoryCommandValidator _validator = new(null!);

    [Test]
    public void Validate_IdIsNotEmpty_WhenIdIsProvided()
    {
        // Arrange
        UpsertCategoryCommand command = new() { Id = Guid.NewGuid(), Name = "Valid Name" };

        // Act
        TestValidationResult<UpsertCategoryCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    [Test]
    public void Validate_IdIsEmptyGuid_WhenIdIsProvided()
    {
        // Arrange
        UpsertCategoryCommand command = new() { Id = Guid.Empty, Name = "Valid Name" };

        // Act
        TestValidationResult<UpsertCategoryCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Id);
    }

    [Test]
    public void Validate_IdAllowsNull()
    {
        // Arrange
        UpsertCategoryCommand command = new() { Id = null, Name = "Valid Name" };

        // Act
        TestValidationResult<UpsertCategoryCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(c => c.Id);
    }

    [Test]
    public void Validate_NameIsNotEmpty()
    {
        // Arrange
        UpsertCategoryCommand command = new() { Id = Guid.NewGuid(), Name = "" };

        // Act
        TestValidationResult<UpsertCategoryCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("'Name' must not be empty.");
    }

    [Test]
    public void Validate_NameExceedsMaxLength()
    {
        // Arrange
        UpsertCategoryCommand command = new()
        {
            Id = Guid.NewGuid(), Name = new string('A', 201) // 201 characters
        };

        // Act
        TestValidationResult<UpsertCategoryCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(c => c.Name)
            .WithErrorMessage("The length of 'Name' must be 200 characters or fewer. You entered 201 characters.");
    }

    [Test]
    public void Validate_ValidCommand_NoValidationErrors()
    {
        // Arrange
        UpsertCategoryCommand command = new() { Id = Guid.NewGuid(), Name = "Valid Name" };

        // Act
        TestValidationResult<UpsertCategoryCommand>? result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}
