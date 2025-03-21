using TodoApi.Dtos;
using TodoApi.Models;

public interface ITodoRepository
{
    Task<IEnumerable<TodoItem>> GetAllAsync(long listId);
    Task<TodoItem> GetByIdAsync(long listId, long itemId);
    Task<TodoItem> AddAsync(long listId, CreateTodoItem item);
    Task<TodoItem> UpdateAsync(long itemId, UpdateTodoItem item);
    Task<bool> DeleteAsync(long itemId);
    Task DeleteAllAsync(long listId);
}
