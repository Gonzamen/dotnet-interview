using TodoApi.Dtos;
using Models = TodoApi.Models;

namespace TodoApi.Services.TodoItem
{
    public interface ITodoItemService
    {
        Task<IEnumerable<Models.TodoItem>> GetAllItemsByListIdAsync(long listId);
        Task<Models.TodoItem> GetItemByIdAsync(long listId, long itemId);
        Task<Models.TodoItem> CreateItemAsync(long listId, CreateTodoItem item);
        Task<Models.TodoItem> UpdateItemAsync(long itemId, UpdateTodoItem item);
        Task<bool> DeleteItemAsync(long itemId);
        Task DeleteAllItemsByListIdAsync(long listId);
    }
}
