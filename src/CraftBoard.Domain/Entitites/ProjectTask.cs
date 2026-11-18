
using CraftBoard.Domain.Enums;

namespace CraftBoard.Domain.Entities;

public class ProjectTask
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public Guid ProjectId { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public ProjectTaskStatus Status { get; private set; }
        = ProjectTaskStatus.Open;

    public DateTime CreatedAtUtc { get; private set; }
        = DateTime.UtcNow;

    private ProjectTask()
    {
    }

    public ProjectTask(
        Guid projectId,
        string title,
        string description)
    {
        if (projectId == Guid.Empty)
        {
            throw new ArgumentException(
                "Project ID cannot be empty.",
                nameof(projectId));
        }

        ProjectId = projectId;
        Rename(title);
        Description = description;
    }

    public void Rename(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Task title cannot be empty.",
                nameof(title));
        }

        var trimmedTitle = title.Trim();

        if (trimmedTitle.Length > 200)
        {
            throw new ArgumentException(
                "Task title cannot exceed 200 characters.",
                nameof(title));
        }

        Title = trimmedTitle;
    }

    public void Start()
    {
        if (Status != ProjectTaskStatus.Open)
        {
            throw new InvalidOperationException(
                "Only open tasks can be started.");
        }

        Status = ProjectTaskStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != ProjectTaskStatus.InProgress)
        {
            throw new InvalidOperationException(
                "Only in-progress tasks can be completed.");
        }

        Status = ProjectTaskStatus.Completed;
    }
}
