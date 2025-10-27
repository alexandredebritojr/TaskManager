using SoftPlan.TaskManager.Application.DTOs;
using SoftPlan.TaskManager.Domain.Entities;
using SoftPlan.TaskManager.Domain.Interfaces;

namespace SoftPlan.TaskManager.Application.Services;

public class TaskService
{
    private readonly ITaskRepository _repo;
    public TaskService(ITaskRepository repo) => _repo = repo;

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto)
    {
        var task = new TaskItem(dto.Title, dto.Description, dto.DueDate, dto.UserId);
        await _repo.AddAsync(task);
        return ToDto(task);
    }

    public async Task<IEnumerable<TaskDto>> GetByUserAsync(Guid userId)
    {
        var tasks = await _repo.GetByUserAsync(userId);
        return tasks.Select(ToDto);
    }

    public async Task CompleteTaskAsync(Guid id)
    {
        var task = await _repo.GetByIdAsync(id);
        if (task == null)
            throw new KeyNotFoundException($"Task with ID {id} not found.");

        task.Complete();
        await _repo.UpdateAsync(task);
    }

        public async Task DeleteTaskAsync(Guid id)
    {
        var task = await _repo.GetByIdAsync(id);
        if (task == null)
            throw new KeyNotFoundException($"Task with ID {id} not found.");

        await _repo.DeleteAsync(id);
    }

    private static TaskDto ToDto(TaskItem t) => new(t.Id, t.Title, t.Description, t.CreatedAt, t.DueDate, t.UserId, t.IsCompleted);
}

