using TodoApi.Dtos;
using Models = TodoApi.Models;

namespace TodoApi.Repositories.TodoItem
{
    public interface ITodoItemRepository
    {
        Task<IEnumerable<Models.TodoItem>> GetAllAsync(long listId);
        Task<Models.TodoItem> GetByIdAsync(long listId, long itemId);
        Task<Models.TodoItem> AddAsync(long listId, CreateTodoItem item);
        Task<Models.TodoItem> UpdateAsync(long itemId, UpdateTodoItem item);
        Task<bool> DeleteAsync(long itemId);
        Task DeleteAllAsync(long listId);
    }
}
