using CloToDo.Models;

namespace CloToDo.Services;

public interface ITodoService
{
        // Task<TodoItem> GetByIdAsync(Guid id);
        Task<List<TodoItem>> GetAllAsync();
        // Task AddAsync(TodoItem todo);
        // Task UpdateAsync(TodoItem todo);
        // Task DeleteByIdAsync(Guid id);
    }
    
