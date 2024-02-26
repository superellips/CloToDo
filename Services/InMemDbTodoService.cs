/// <summary>
/// Represents an in-memory database implementation of the ITodoService interface.
/// This class provides methods to perform CRUD operations on a collection of TodoItems.
/// 
/// 1. It simulates asynchronous operations using the Task-based Asynchronous Pattern (TAP).
/// 2. It locks the list of TodoItems to ensure thread safety.
/// 3. It copies the TodoItems to ensure immutability.
/// 
/// The InMemDbTodoService class should be registered as Singleton in the DI container.
/// 
/// Error handling: KeyNotFoundException is thrown when a TodoItem with the specified Id is not found.
/// 
/// </summary>

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CloToDo.Models;

namespace CloToDo.Services;

public class InMemDbTodoService : ITodoService
{
    private readonly List<TodoItem> _todos;
    private readonly object _lock = new(); // For thread safety

    public InMemDbTodoService()
    {
        _todos =
        [
            new() { Id = Guid.NewGuid(), Title = "Learn C#", IsComplete = false },
            new() { Id = Guid.NewGuid(), Title = "Learn Azure", IsComplete = true },
            new() { Id = Guid.NewGuid(), Title = "Build apps for the cloud", IsComplete = false }
        ];
    }

    public async Task<IEnumerable<TodoItem>> GetAllAsync()
    {
        // Lock the list of TodoItems to ensure thread safety
        var todoList = new List<TodoItem>();
        lock (_lock)
        {
            // Copy the list of TodoItems to ensure immutability
            foreach (var originalTodoItem in _todos)
            {
                var todo = new TodoItem
                {
                    Id = originalTodoItem.Id,
                    Title = originalTodoItem.Title,
                    IsComplete = originalTodoItem.IsComplete
                };
                todoList.Add(todo);
            }
        }

        // Return a copy of the list of TodoItems to ensure immutability
        return await Task.FromResult(todoList);
    }

    public async Task<TodoItem> GetByIdAsync(Guid id)
    {
        // Lock the list of TodoItems to ensure thread safety
        var todo = new TodoItem();
        lock (_lock)
        {
            // Copy the TodoItem with the specified Id to ensure immutability
            var originalTodo = _todos.FirstOrDefault(t => t.Id == id) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found");
            todo.Id = originalTodo.Id;
            todo.Title = originalTodo.Title;
            todo.IsComplete = originalTodo.IsComplete;
        }

        // Return a copy of the TodoItem to ensure immutability
        return await Task.FromResult(todo);
    }

    public async Task<TodoItem> CreateAsync(TodoItem item)
    {
        // Lock the list of TodoItems to ensure thread safety
        lock (_lock)
        {
            // Set the Id of the new TodoItem
            item.Id = Guid.NewGuid();

            // Add the new TodoItem to the list
            _todos.Add(item);
        }

        // Return a copy of the new TodoItem to ensure immutability
        return await GetByIdAsync(item.Id);
    }

    public async Task<TodoItem> UpdateAsync(Guid id, TodoItem item)
    {
        // Lock the list of TodoItems to ensure thread safety
        lock (_lock)
        {
            // Find the TodoItem with the specified Id
            var todo = _todos.FirstOrDefault(t => t.Id == id) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found");

            // Update the TodoItem
            todo.Title = item.Title;
            todo.IsComplete = item.IsComplete;
        }

        // Return a copy of the updated TodoItem to ensure immutability
        return await GetByIdAsync(id);
    }

    public async Task<TodoItem> DeleteAsync(Guid id)
    {
        // Lock the list of TodoItems to ensure thread safety
        var todo = new TodoItem();
        lock (_lock)
        {
            // Find the TodoItem with the specified Id
            var originalTodo = _todos.FirstOrDefault(t => t.Id == id) ?? throw new KeyNotFoundException($"TodoItem with Id {id} not found");

            // Copy the TodoItem to ensure immutability
            todo.Id = originalTodo.Id;
            todo.Title = originalTodo.Title;
            todo.IsComplete = originalTodo.IsComplete;

            // Remove the TodoItem from the list
            _todos.Remove(originalTodo);

        }

        // Return information about the deleted TodoItem
        return await Task.FromResult(todo);
    }
}
