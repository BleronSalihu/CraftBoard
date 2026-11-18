using CraftBoard.Application.Interfaces;
using CraftBoard.Domain.Entities;

namespace CraftBoard.Application.Services;

public class ProjectService(IProjectRepository projectRepository, ICurrentUser currentUser)
{
    public async Task<Project> CreateAsync(string name, string description)
    {
        var ownerId = await currentUser.GetRequiredUserIdAsync();
        var project = new Project(ownerId, name, description);
        await projectRepository.AddAsync(project);
        return project;
    }

    public async Task<List<Project>> GetAllAsync()
    {
        var ownerId = await currentUser.GetRequiredUserIdAsync();
        return await projectRepository.GetAllByOwnerAsync(ownerId);
    }

    public async Task<Project?> GetByIdAsync(Guid projectId)
    {
        var ownerId = await currentUser.GetRequiredUserIdAsync();
        return await projectRepository.GetByIdAsync(projectId, ownerId);
    }
}
