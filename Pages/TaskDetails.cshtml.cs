using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CloToDo.Models; 
using CloToDo.Services; 

namespace CloToDo.Pages;

public class TaskDetailsModel : PageModel
{
    private readonly ILogger<TaskDetailsModel> _logger;
    private readonly ITodoService _todoService;
    private readonly ITodoImageService _todoImageService;

    // This property binds to the route data automatically.
    // [BindProperty(SupportsGet = true)]
    // public Guid Id { get; set; }

    public TodoItem TodoItem { get; set; } = new TodoItem();

    public TaskDetailsModel(ILogger<TaskDetailsModel> logger, ITodoService todoService, ITodoImageService todoImageService)
    {
        _logger = logger;
        _todoService = todoService;
        _todoImageService = todoImageService;
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

    public async Task<IActionResult> OnPostAsync(Guid id, IFormFile imageFile)
    {
        var todoItem = await _todoService.GetByIdAsync(id);

        if (imageFile != null)
        {
           todoItem.ImageUrl = await _todoImageService.UploadImageAsync(imageFile);
           await _todoService.UpdateAsync(id, todoItem);
        }

        return RedirectToPage(); // Refresh the page to show the updated details
    }

}
