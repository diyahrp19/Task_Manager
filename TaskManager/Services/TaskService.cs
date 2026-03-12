using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public Task<List<TaskItem>> GetAllAsync() => _repository.GetAllAsync();

    public Task<TaskItem?> GetByIdAsync(string id) => _repository.GetByIdAsync(id);

    public async Task CreateAsync(TaskItem task)
    {
        // Basic validation
        if (string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("Title is required");
        await _repository.CreateAsync(task);
    }

    public async Task UpdateAsync(string id, TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
            throw new ArgumentException("Title is required");
        await _repository.UpdateAsync(id, task);
    }

    public Task DeleteAsync(string id) => _repository.DeleteAsync(id);

    public Task<List<TaskItem>> GetByStatusAsync(Status status) => _repository.GetByStatusAsync(status);

    public Task<List<TaskItem>> GetByPriorityAsync(Priority priority) => _repository.GetByPriorityAsync(priority);

    public Task<List<TaskItem>> SearchAsync(string query) => _repository.SearchAsync(query ?? string.Empty);
}
