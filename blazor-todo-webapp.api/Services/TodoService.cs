using blazor_todo_webapp.api.Model;

namespace blazor_todo_webapp.api.Services;

public class TodoService
{
    private readonly List<Todo> _todos = new();
    private int _nextId = 1;

    public TodoService()
    {
        Add(new Todo { Title = "Walk the dog" });
        Add(new Todo { Title = "Do the dishes" });
        Add(new Todo { Title = "Clean the bathroom" });
    }

    public List<Todo> GetAll() => _todos;

    public Todo? GetById(int id) => _todos.FirstOrDefault(t => t.Id == id);

    public Todo Add(Todo todo)
    {
        todo.Id = _nextId++;
        todo.CreatedAt = DateTime.UtcNow;
        _todos.Add(todo);
        return todo;
    }

    public bool Update(int id, Todo updated)
    {
        var todo = GetById(id);
        if (todo is null) return false;

        todo.Title = updated.Title;
        todo.IsComplete = updated.IsComplete;
        return true;
    }

    public bool Delete(int id)
    {
        var todo = GetById(id);
        if (todo is null) return false;

        _todos.Remove(todo);
        return true;
    }
}
