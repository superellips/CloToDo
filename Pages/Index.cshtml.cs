using CloToDo.Models;
using CloToDo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CloToDo.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ITodoService _todoService;
    public IEnumerable<TodoItem> TodoItems { get; set; } = [];

    public IndexModel(ILogger<IndexModel> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    public async Task<IActionResult> OnGetAsync()
    {
        // Use the ITodoService to get all the TodoItems
        TodoItems = await _todoService.GetAllAsync();

        // Return the Page with the list of TodoItems
        return Page();
    }
}
