using TaskManager.Models;
using TaskManager.Data;

namespace TaskManager.Services;

public interface ITaskService
{
    Task<List<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(string id);
    Task CreateAsync(TaskItem task);
    Task UpdateAsync(string id, TaskItem task);
    Task DeleteAsync(string id);
    Task<List<TaskItem>> GetByStatusAsync(Status status);
    Task<List<TaskItem>> GetByPriorityAsync(Priority priority);
    Task<List<TaskItem>> SearchAsync(string query);
}
