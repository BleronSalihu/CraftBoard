using BlazorApp1.Data;
using CraftBoard.Application.Interfaces;
using CraftBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Data.Repositories;

public class ProjectRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IProjectRepository
{
    public async Task<List<Project>> GetAllByOwnerAsync(
        string ownerId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Projects.AsNoTracking().Where(p => p.OwnerId == ownerId).OrderBy(p => p.Name).ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid projectId, string ownerId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.Projects.AsNoTracking().FirstOrDefaultAsync(p => p.Id == projectId && p.OwnerId == ownerId);
    }

    public async Task AddAsync(Project project)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        await context.Projects.AddAsync(project);
        await context.SaveChangesAsync();
    }
}
