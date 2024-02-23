using System;
using System.Collections.Generic;
using CloToDo.Models;

namespace CloToDo.Services;

public class InMemDbTodoService : ITodoService
{
    private List<TodoItem> _todos;

    public InMemDbTodoService()
    {
        _todos = new List<TodoItem>()
        {
            new TodoItem { Id = Guid.NewGuid(), Description = "Learn more C#", IsComplete = false },
            new TodoItem { Id = Guid.NewGuid(), Description = "Build better apps", IsComplete = true },
            new TodoItem { Id = Guid.NewGuid(), Description = "Make more money", IsComplete = false },
            new TodoItem { Id = Guid.NewGuid(), Description = "Retire earlier", IsComplete = false }
        };
    }

    public Task<List<TodoItem>> GetAllAsync()
    {
        return Task.FromResult(_todos);
    }

//     public TodoItem GetByIdAsync(Guid id)
//     {
//         return _todos.Find(todo => todo.Id == id) ?? new TodoItem();
//     }

//     public void AddTodoItem(TodoItem todo)
//     {
//         if (todo == null)
//         {
//             throw new ArgumentNullException(nameof(todo));
//         }

//         todo.Id = _todos.Count + 1;
//         _todos.Add(todo);
//     }

//     public void UpdateTodoItem(TodoItem todo)
//     {
//         if (todo == null)
//         {
//             throw new ArgumentNullException(nameof(todo));
//         }

//         var existingTodo = _todos.Find(t => t.Id == todo.Id);
//         if (existingTodo != null)
//         {
//             existingTodo.Title = todo.Title;
//             existingTodo.Description = todo.Description;
//             existingTodo.IsCompleted = todo.IsCompleted;
//         }
//     }

//     public void DeleteTodoItem(Guid id)
//     {
//         var todo = _todos.Find(t => t.Id == id);
//         if (todo != null)
//         {
//             _todos.Remove(todo);
//         }
//     }
// }
}
