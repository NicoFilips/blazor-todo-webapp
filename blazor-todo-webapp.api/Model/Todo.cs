namespace blazor_todo_webapp.api.Model;

public class Cache
{
    public static List<Todo> _todos = new();
}

public class Todo : IDisposable
{
    public Todo(int id, string? title)
    {
        Title = title;
        Id = id;
        CreateDate = DateTime.Now;
    }

    public string? Title { get; init; }

    public int Id { get; init; }

    public DateTime? CreateDate { get; init; }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}