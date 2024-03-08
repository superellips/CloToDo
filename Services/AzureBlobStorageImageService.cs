using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using CloToDo.Configuration;

namespace CloToDo.Services;

public class AzureBlobTodoImageService : ITodoImageService
{
    private readonly BlobContainerClient _blobContainerClient;

    public AzureBlobTodoImageService(IOptions<BlobStorageSettings> settings)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(settings.Value.ConnectionString);
        _blobContainerClient = blobServiceClient.GetBlobContainerClient(settings.Value.ContainerName);
    }

    public async Task<string> UploadImageAsync(IFormFile file) 
    {
        string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var blobClient = _blobContainerClient.GetBlobClient(uniqueFileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, new BlobHttpHeaders { ContentType = file.ContentType });
        }

        return blobClient.Uri.ToString(); 
    }
}
