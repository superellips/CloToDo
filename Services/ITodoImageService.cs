namespace CloToDo.Services;

public interface ITodoImageService
{
    public Task<string> UploadImageAsync(IFormFile file);
}