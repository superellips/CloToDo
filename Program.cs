using CloToDo.Configuration;
using CloToDo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (Dependency Injection).
builder.Services.AddRazorPages();

// Add the MongoDB settings to the configuration
builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDBSettings"));

// Add the BlobStorage settings to the configuration
builder.Services.Configure<BlobStorageSettings>(builder.Configuration.GetSection("BlobStorageSettings"));

// Add Azure image service
builder.Services.AddSingleton<ITodoImageService, AzureBlobTodoImageService>();

// Read the TodoServiceImplementation from the configuration
var todoServiceImplementation = builder.Configuration.GetValue<string>("TodoServiceImplementation");

switch (todoServiceImplementation)
{
    case "MongoDb":
        builder.Services.AddSingleton<ITodoService, MongoDbTodoService>();
        Console.WriteLine("Using MongoDB TodoService");
        break;
    case "InMemDb":
    default:
        builder.Services.AddSingleton<ITodoService, InMemDbTodoService>();
        Console.WriteLine("Using InMemDB TodoService");
        break;
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
