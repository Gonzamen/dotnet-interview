using TodoApi.Dtos;
using TodoApi.Models;

public interface ITodoService
{
    Task<IEnumerable<TodoItem>> GetAllItemsByListIdAsync(long listId);
    Task<TodoItem> GetItemByIdAsync(long listId, long itemId);
    Task<TodoItem> CreateItemAsync(long listId, CreateTodoItem item);
    Task<TodoItem> UpdateItemAsync(long itemId, UpdateTodoItem item);
    Task<bool> DeleteItemAsync(long itemId);
    Task DeleteAllItemsByListIdAsync(long listId);
}
