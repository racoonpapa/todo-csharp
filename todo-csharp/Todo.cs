using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace todo_csharp;

public enum Priority
{
    None = 0,
    Low = 1,
    Medium = 2,
    High =3
}

public class TodoItem
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("content")]
    public string Content { get; set; }
    [JsonPropertyName("due_date")]
    public DateTime? DueDate { get; set; }  = null;
    [JsonPropertyName("priority")]
    public Priority Priority { get; set; }
    [JsonPropertyName("done")]
    public bool Done { get; set; }

    public TodoItem(string content)
    {
        Id = Guid.NewGuid().ToString();
        Content = content;
    }

    public TodoItem WithDueDate(DateTime? dueDate)
    {
        DueDate = dueDate;
        return this;
    }

    public TodoItem WithPriority(Priority priority)
    {
        Priority = priority;
        return this;
    }

    public override string ToString()
    {
        var builder = new StringBuilder($"[{(Done ? 'x' : ' ')}] {Content}");

        if (Priority != Priority.None) builder.Append($" [{Priority}]");
        if (DueDate != null) builder.Append($" ({DueDate?.ToString("yyyy-MM-dd")})");
        
        return builder.ToString();
    }
}

public class TodoList
{
    private Dictionary<string, TodoItem> _items = new Dictionary<string, TodoItem>();

    // .NET Core 3.0+, .NET 5/6 or higher required 
    public static TodoList? FromJson(string json)
    {
        if (string.IsNullOrEmpty(json))
            return null;

        try
        {
            var items = JsonSerializer.Deserialize<TodoItem[]>(json);
            if (items == null) return null;

            var newList = new TodoList();
            foreach (var item in items) newList._items.Add(item.Id, item);

            return newList;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(_items.Values);
    }

    public void Print()
    {
        foreach(var item in _items) Console.WriteLine(item.Value.ToString());
    }
}