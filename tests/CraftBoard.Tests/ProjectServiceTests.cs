
using CraftBoard.Application.Interfaces;
using CraftBoard.Application.Services;
using CraftBoard.Domain.Entities;

namespace CraftBoard.Tests;

public class ProjectServiceTests
{
    [Fact]
    public async Task CreateAsync_AssignsCurrentUserAsOwner()
    {
        // Arrange
        var repository = new FakeProjectRepository();
        var currentUser = new FakeCurrentUser("user-A");

        var service = new ProjectService(
            repository,
            currentUser);

        // Act
        var project = await service.CreateAsync(
            "Mein Projekt",
            "Beschreibung");

        // Assert
        Assert.Equal("user-A", project.OwnerId);
        Assert.Single(repository.Projects);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsOnlyOwnProjects()
    {
        // Arrange
        var repository = new FakeProjectRepository();

        await repository.AddAsync(
            new Project("user-A", "Projekt A", ""));

        await repository.AddAsync(
            new Project("user-B", "Projekt B", ""));

        var service = new ProjectService(
            repository,
            new FakeCurrentUser("user-A"));

        // Act
        var projects = await service.GetAllAsync();

        // Assert
        Assert.Single(projects);
        Assert.Equal("Projekt A", projects[0].Name);
    }

    [Fact]
    public async Task GetByIdAsync_OtherUsersProject_ReturnsNull()
    {
        // Arrange
        var repository = new FakeProjectRepository();

        var foreignProject = new Project(
            "user-B",
            "Privates Projekt",
            "");

        await repository.AddAsync(foreignProject);

        var service = new ProjectService(
            repository,
            new FakeCurrentUser("user-A"));

        // Act
        var result = await service.GetByIdAsync(
            foreignProject.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateAsync_UnauthenticatedUser_Throws()
    {
        // Arrange
        var service = new ProjectService(
            new FakeProjectRepository(),
            new FakeCurrentUser(null));

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(
            () => service.CreateAsync("Test", ""));
    }

    private sealed class FakeCurrentUser(
        string? userId) : ICurrentUser
    {
        public Task<string> GetRequiredUserIdAsync()
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new UnauthorizedAccessException();
            }

            return Task.FromResult(userId);
        }
    }

    private sealed class FakeProjectRepository
        : IProjectRepository
    {
        public List<Project> Projects { get; } = [];

        public Task<List<Project>> GetAllByOwnerAsync(
            string ownerId)
        {
            var result = Projects
                .Where(p => p.OwnerId == ownerId)
                .ToList();

            return Task.FromResult(result);
        }

        public Task<Project?> GetByIdAsync(
            Guid projectId,
            string ownerId)
        {
            var project = Projects.FirstOrDefault(
                p => p.Id == projectId &&
                     p.OwnerId == ownerId);

            return Task.FromResult(project);
        }

        public Task AddAsync(Project project)
        {
            Projects.Add(project);

            return Task.CompletedTask;
        }
    }
}
