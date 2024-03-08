using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CloToDo.Models; 
using CloToDo.Services; 

namespace CloToDo.Pages;

public class TaskDetailsModel : PageModel
{
    private readonly ILogger<TaskDetailsModel> _logger;
    private readonly ITodoService _todoService;

    // This property binds to the route data automatically.
    // [BindProperty(SupportsGet = true)]
    // public Guid Id { get; set; }

    public TodoItem TodoItem { get; set; } = new TodoItem();

    public TaskDetailsModel(ILogger<TaskDetailsModel> logger, ITodoService todoService)
    {
        _logger = logger;
        _todoService = todoService;
    }

    // public async Task<IActionResult> OnGetAsync()
    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        TodoItem = await _todoService.GetByIdAsync(id);

        if (TodoItem == null)
        {
            return NotFound();
        }

        return Page();
    }
}
