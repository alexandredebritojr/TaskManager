using System;

namespace SoftPlan.TaskManager.Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? DueDate { get; private set; }
    public Guid UserId { get; private set; }
    public bool IsCompleted { get; private set; } = false;

    // Constructor for creation
    public TaskItem(string title, string description, DateTime? dueDate, Guid userId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        UserId = userId;
    }

    private TaskItem() { } // for EF

    public void Complete() => IsCompleted = true;
}

