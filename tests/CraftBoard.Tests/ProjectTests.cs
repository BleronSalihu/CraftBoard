
using CraftBoard.Domain.Entities;
namespace CraftBoard.Tests;

public class ProjectTests
{
    [Fact]
    public void Constructor_WithValidName_CreatesProject()
    {
        // Arrange
        var name = "ProjectFlow";
        var description = "Projektmanagement-System";

        // Act
        var project = new Project(name, description);

        // Assert
        Assert.Equal(name, project.Name);
        Assert.Equal(description, project.Description);
        Assert.NotEqual(Guid.Empty, project.Id);
    }

    [Theory]
    [InlineData("  Neues Projekt  ", "Neues Projekt")]
    [InlineData("Test", "Test")]
    public void Rename_WithValidName_UpdatesName(
        string input,
        string expected)
    {
        // Arrange
        var project = new Project("Original", "Beschreibung");

        // Act
        project.Rename(input);

        // Assert
        Assert.Equal(expected, project.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Rename_WithInvalidName_ThrowsException(
        string invalidName)
    {
        // Arrange
        var project = new Project("Original", "Beschreibung");

        // Act
        Action action = () => project.Rename(invalidName);

        // Assert
        Assert.Throws<ArgumentException>(action);
    }
}
