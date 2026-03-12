using MongoDB.Driver;
using TaskManager.Models;

namespace TaskManager.Data;

public interface IMongoDbContext
{
    IMongoCollection<TaskItem> Tasks { get; }
}

public class MongoDbContext : IMongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoDatabase database)
    {
        _database = database;
    }

    public IMongoCollection<TaskItem> Tasks => _database.GetCollection<TaskItem>("tasks");
}
