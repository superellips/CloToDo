using System.ComponentModel.DataAnnotations;

namespace CloToDo.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    [Required(ErrorMessage = "The Title field is required")]
    public string? Title { get; set; }
    public bool IsComplete { get; set; }
}
