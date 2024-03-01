using CloToDo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container (Dependency Injection).
builder.Services.AddRazorPages();

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
