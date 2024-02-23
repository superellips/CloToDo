using CloToDo.Models;
using CloToDo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CloToDo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITodoService _todoService;
    public List<TodoItem> TodoItems { get; set; } = [];

    public IndexModel(ILogger<IndexModel> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    public async void OnGet()
    {
        // Use the ITodoService to get all the TodoItems
        TodoItems = await _todoService.GetAllAsync();
    }
}
