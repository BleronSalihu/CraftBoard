using System;
using System.Collections.Generic;
using System.Text;
using CraftBoard.Domain.Entities;

namespace CraftBoard.Application.Interfaces;

public interface IProjectRepository
{
    Task<List<Project>> GetAllByOwnerAsync(string ownerId);

    Task<Project?> GetByIdAsync(Guid projectId, string ownerId);

    Task AddAsync(Project project);
}
