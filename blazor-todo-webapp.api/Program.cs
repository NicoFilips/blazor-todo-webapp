using System.Text.Json.Serialization;
using blazor_todo_webapp.api.Model;
using blazor_todo_webapp.api.Util;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

var app = builder.Build();

var sampleTodos = new Todo[]
{
    new(1, "Walk the dog"),
    new(2, "Do the dishes"),
    new(3, "Do the laundry"),
    new(4, "Clean the bathroom"),
    new(5, "Clean the car")
};

app.MapGet("/todoid", (int id) => TodoUtil.GetTodoById(id))
    .WithName("todoid");

app.MapGet("/randomtodo", () => TodoUtil.GetRandomTodo())
    .WithName("GetRandomTodo");

app.MapPost("/todo", (int id, string? title) =>
{
    var todo = new Todo(id,title);
    Cache._todos.Add(todo);
    // Beispiel: SaveTodo(todo);
    return Results.Created($"/todo/{id}", todo);
});

app.MapDelete("/todo/{id}", (int id) =>
{
    TodoUtil.DeleteTodoById(id);
    // Beispiel: DeleteTodoById(id);
    return Results.Ok($"Todo mit der ID {id} wurde gelöscht.");
});


var todosApi = app.MapGroup("/todos");
todosApi.MapGet("/", () => sampleTodos);
todosApi.MapGet("/{id}", (int id) =>
    sampleTodos.FirstOrDefault(a => a.Id == id) is { } todo
        ? Results.Ok(todo)
        : Results.NotFound());

app.Run();

[JsonSerializable(typeof(Todo[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{
}