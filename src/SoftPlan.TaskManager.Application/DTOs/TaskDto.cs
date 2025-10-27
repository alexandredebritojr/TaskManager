namespace SoftPlan.TaskManager.Application.DTOs;

public record TaskDto(Guid Id, string Title, string Description, DateTime CreatedAt, DateTime? DueDate, Guid UserId, bool IsCompleted);

