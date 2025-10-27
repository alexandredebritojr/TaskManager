using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SoftPlan.TaskManager.Domain.Entities;

namespace SoftPlan.TaskManager.Domain.Interfaces;

public interface ITaskRepository
{
    Task<TaskItem> AddAsync(TaskItem task);
    Task<IEnumerable<TaskItem>> GetByUserAsync(Guid userId);
    Task<TaskItem?> GetByIdAsync(Guid id);
    Task UpdateAsync(TaskItem task);
    Task<bool> DeleteAsync(Guid id);
    Task SaveChangesAsync();
}

