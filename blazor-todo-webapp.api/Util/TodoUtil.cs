namespace blazor_todo_webapp.api.Util;

public class TodoUtil
{
    public static Todo GetRandomTodo()
    {
        var todo = new Todo(title: "Todo " + Random.Shared.Next(1, 100), id: Random.Shared.Next(1, 100));
        return todo;
    }


    public static void DeleteTodoById(int id)
    {
        Todo todo = Cache._todos.FirstOrDefault(t => t.Id == id);
        todo?.Dispose();
        Console.WriteLine($"Delete todo {todo.Id}");
    }

    public static Todo GetTodoById(int id)
    {
        return Cache._todos.FirstOrDefault(t => t.Id == id);
    }
}