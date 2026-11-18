using System;
using System.Collections.Generic;
using System.Text;



namespace CraftBoard.Domain.Entities;

public class Project
{
    public Guid Id { get; private set; } = Guid.NewGuid();

    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public DateTime CreatedAtUtc { get; private set; }
        = DateTime.UtcNow;

    private Project()
    {
    }

    public Project(string name, string description)
    {
        Rename(name);
        Description = description;
    }


    public void Rename(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Project name cannot be empty.",
                nameof(name));
        }

        var trimmedName = name.Trim();

        if (trimmedName.Length > 100)
        {
            throw new ArgumentException(
                "Project name cannot exceed 100 characters.",
                nameof(name));
        }

        Name = trimmedName;
    }

}
