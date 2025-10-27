using Microsoft.EntityFrameworkCore;
using SoftPlan.TaskManager.Domain.Entities;
using SoftPlan.TaskManager.Domain.Interfaces;
using SoftPlan.TaskManager.Infrastructure.Data;

namespace SoftPlan.TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;
    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<TaskItem> AddAsync(TaskItem task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<IEnumerable<TaskItem>> GetByUserAsync(Guid userId) =>
        await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();

    public async Task<TaskItem?> GetByIdAsync(Guid id) =>
        await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id);

    public async Task UpdateAsync(TaskItem task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task == null) return false;
        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}

