using TodoApi.Dtos;
using Models = TodoApi.Models;

namespace TodoApi.Services.TodoList
{
    public interface ITodoListService
    {
        Task<IEnumerable<Models.TodoList>> GetAllListsAsync();
        Task<Models.TodoList> GetListByIdAsync(long listId);
        Task<Models.TodoList> CreateListAsync(CreateTodoList list);
        Task<Models.TodoList> UpdateListAsync(long listId, UpdateTodoList list);
        Task<bool> DeleteListAsync(long listId);
    }
}
