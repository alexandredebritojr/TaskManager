namespace SoftPlan.TaskManager.Application.DTOs;

public record CreateTaskDto(string Title, string Description, DateTime? DueDate, Guid UserId);

