namespace TodoApi.Models;

public class TodoItem
{
    public long Id { get; set; }
    public required string Name { get; set; }
    public long ListId { get; set; }
    public TodoList List { get; set; } = default!;
    public bool IsComplete { get; set; }
}
