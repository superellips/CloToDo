using CloToDo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CloToDo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    public List<TodoItem> TodoItems { get; set; } = [];

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        Console.WriteLine("Hello World!");
        TodoItems = new List<TodoItem>
        {
            new TodoItem { Id = Guid.NewGuid(), Description = "Learn C#", IsComplete = false },
            new TodoItem { Id = Guid.NewGuid(), Description = "Build apps", IsComplete = true },
            new TodoItem { Id = Guid.NewGuid(), Description = "Make money", IsComplete = false },
            new TodoItem { Id = Guid.NewGuid(), Description = "Retire", IsComplete = false }
        };

    }
}
