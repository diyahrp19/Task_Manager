using MongoDB.Driver;
using TaskManager.Data;
using TaskManager.Services;
using TaskManager.Models;
using TaskManager.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddControllersWithViews();

// MongoDB configuration
var mongoConfig = builder.Configuration.GetSection("MongoDb");
var connectionString = mongoConfig["ConnectionString"] ?? "mongodb://localhost:27017";
var databaseName = mongoConfig["DatabaseName"] ?? "TaskManagerDb";

builder.Services.AddSingleton<IMongoClient>(new MongoClient(connectionString));
builder.Services.AddScoped<IMongoDbContext>(provider =>
{
    var client = provider.GetRequiredService<IMongoClient>();
    return new MongoDbContext(client.GetDatabase(databaseName));
});
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "tasks",
    pattern: "tasks/{action=Index}/{id?}",
    defaults: new { controller = "Tasks" });
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
