
using CraftBoard.Domain.Entities;

namespace CraftBoard.Tests;

public class ProjectTests
{
    [Fact]
    public void Constructor_WithValidData_CreatesProject()
    {
        var ownerId = "user-123";

        var project = new Project(
            ownerId,
            "CraftBoard",
            "Projektmanagement");

        Assert.Equal(ownerId, project.OwnerId);
        Assert.Equal("CraftBoard", project.Name);
        Assert.NotEqual(Guid.Empty, project.Id);
    }

    [Theory]
    [InlineData("  Neues Projekt  ", "Neues Projekt")]
    [InlineData("Test", "Test")]
    public void Rename_WithValidName_UpdatesName(
        string input,
        string expected)
    {
        var project = new Project(
            "user-123",
            "Original",
            "Beschreibung");

        project.Rename(input);

        Assert.Equal(expected, project.Name);
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("   ")]
    public void Rename_WithInvalidName_ThrowsException(
        string invalidName)
    {
        var project = new Project(
            "user-123",
            "Original",
            "Beschreibung");

        Assert.Throws<ArgumentException>(
            () => project.Rename(invalidName));
    }

    [Fact]
    public void Constructor_WithoutOwner_ThrowsException()
    {
        Assert.Throws<ArgumentException>(
            () => new Project(
                "",
                "Test",
                "Beschreibung"));
    }
}
