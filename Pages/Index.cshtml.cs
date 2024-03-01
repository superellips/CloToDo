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

    // Model binding for the "Add Todo" form
    [BindProperty]
    public TodoItem NewTodo { get; set; } = new();

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

    public async Task<IActionResult> OnPostAddTodoAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Use the ITodoService to create a new TodoItem
        await _todoService.CreateAsync(NewTodo);
        
        // Redirect to the same page to refresh the list of TodoItems
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostToggleTodoIsCompleteAsync(Guid Id)
    {
        // Use the ITodoService to get the TodoItem by Id
        var todo = await _todoService.GetByIdAsync(Id);

        // Toggle the IsComplete property of the TodoItem
        todo.IsComplete = !todo.IsComplete;

        // Use the ITodoService to update the TodoItem
        await _todoService.UpdateAsync(Id, todo);

        // Redirect to the same page to refresh the list of TodoItems
        return RedirectToPage();
    }

}
