using TodoApi.Dtos;
using Models = TodoApi.Models;

namespace TodoApi.Repositories.TodoList
{
    public interface ITodoListRepository
    {
        Task<IEnumerable<Models.TodoList>> GetAllAsync();
        Task<Models.TodoList> GetByIdAsync(long listId);
        Task<Models.TodoList> CreateAsync(CreateTodoList list);
        Task<Models.TodoList> UpdateAsync(long listId, UpdateTodoList list);
        Task<bool> DeleteAsync(long listId);
    }
}
