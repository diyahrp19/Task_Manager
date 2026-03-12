using MongoDB.Bson;
using MongoDB.Driver;
using TaskManager.Models;
using TaskManager.Data;

namespace TaskManager.Data;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskRepository(IMongoDbContext context)
    {
        _tasks = context.Tasks;
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _tasks.Find(_ => true).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(string id)
    {
        return await _tasks.Find(t => t.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateAsync(TaskItem task)
    {
        task.CreatedAt = DateTime.UtcNow;
        task.UpdatedAt = DateTime.UtcNow;
        await _tasks.InsertOneAsync(task);
    }

    public async Task UpdateAsync(string id, TaskItem task)
    {
        task.UpdatedAt = DateTime.UtcNow;
        var filter = Builders<TaskItem>.Filter.Eq(t => t.Id, id);
        await _tasks.ReplaceOneAsync(filter, task);
    }

    public async Task DeleteAsync(string id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(t => t.Id, id);
        await _tasks.DeleteOneAsync(filter);
    }

    public async Task<List<TaskItem>> GetByStatusAsync(Status status)
    {
        return await _tasks.Find(t => t.Status == status).ToListAsync();
    }

    public async Task<List<TaskItem>> GetByPriorityAsync(Priority priority)
    {
        return await _tasks.Find(t => t.Priority == priority).ToListAsync();
    }

    public async Task<List<TaskItem>> SearchAsync(string query)
    {
        var filter = Builders<TaskItem>.Filter.Or(
            Builders<TaskItem>.Filter.Regex(t => t.Title, new BsonRegularExpression(query, "i")),
            Builders<TaskItem>.Filter.Regex(t => t.Description, new BsonRegularExpression(query, "i"))
        );
        return await _tasks.Find(filter).ToListAsync();
    }
}
