using blazor_todo_webapp.api.Model;
using blazor_todo_webapp.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<TodoService>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build();

app.UseCors();

var api = app.MapGroup("/api/todos");

api.MapGet("/", (TodoService svc) => svc.GetAll());

api.MapGet("/{id}", (int id, TodoService svc) =>
    svc.GetById(id) is { } todo ? Results.Ok(todo) : Results.NotFound());

api.MapPost("/", (Todo todo, TodoService svc) =>
{
    var created = svc.Add(todo);
    return Results.Created($"/api/todos/{created.Id}", created);
});

api.MapPut("/{id}", (int id, Todo updated, TodoService svc) =>
    svc.Update(id, updated) ? Results.NoContent() : Results.NotFound());

api.MapDelete("/{id}", (int id, TodoService svc) =>
    svc.Delete(id) ? Results.NoContent() : Results.NotFound());

app.Run();
