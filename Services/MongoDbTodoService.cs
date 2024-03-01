using MongoDB.Driver;
using CloToDo.Models;

namespace CloToDo.Services;

public class MongoDbTodoService : ITodoService
{
    private readonly IMongoCollection<TodoItem> _todoItems;

    public MongoDbTodoService()
    {
        // Create a MongoClient object by passing the connection string
        var client = new MongoClient("mongodb://localhost:27017"); // Hardcoded connection string

        // Get the database (creates if it doesn't exist)
        var database = client.GetDatabase("TodoDb");

        // Get the collection (creates if it doesn't exist)
        _todoItems = database.GetCollection<TodoItem>("TodoItems");
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        return await _todoItems.Find(item => true).ToListAsync();
    }

    public async Task<TodoItem> GetByIdAsync(Guid id)
    {
        return await _todoItems.Find(item => item.Id == id).FirstOrDefaultAsync();
    }

    public async Task<TodoItem> CreateAsync(TodoItem item)
    {        
        if (item.Id == Guid.Empty) // Ensure a new ID is generated if not provided
        {
            item.Id = Guid.NewGuid();
        }

        await _todoItems.InsertOneAsync(item);
        return item; // After insertion, the item will have the Id set by MongoDB.
    }

    public async Task<TodoItem> UpdateAsync(Guid id, TodoItem item)
    {
    {
        await _todoItems.ReplaceOneAsync(t => t.Id == id, item);
        return item;
    }
    }

    public async Task<TodoItem> DeleteAsync(Guid id)
    {
        return await _todoItems.FindOneAndDeleteAsync(item => item.Id == id);
    }
}

