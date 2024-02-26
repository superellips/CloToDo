using CloToDo.Models;

namespace CloToDo.Services;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetAllAsync();
    Task<TodoItem> GetByIdAsync(Guid id);
    Task<TodoItem> CreateAsync(TodoItem item);
    Task<TodoItem> UpdateAsync(Guid id, TodoItem item);
    Task<TodoItem> DeleteAsync(Guid id);
}
    
