using TodoApi.Dtos;
using Models = TodoApi.Models;
using TodoApi.Repositories.TodoList;

namespace TodoApi.Services.TodoList
{
    public class TodoListService : ITodoListService
    {
        private readonly ITodoListRepository _repository;

        public TodoListService(ITodoListRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.TodoList>> GetAllListsAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Models.TodoList> GetListByIdAsync(long listId)
        {
            return await _repository.GetByIdAsync(listId);
        }

        public async Task<Models.TodoList> CreateListAsync(CreateTodoList list)
        {
            var todoList = await _repository.CreateAsync(list);
            return todoList;
        }

        public async Task<Models.TodoList> UpdateListAsync(long listId, UpdateTodoList list)
        {
            try
            {
                var todoList = await _repository.UpdateAsync(listId, list);
                return todoList;
            }
            catch (Exception)
            {
                throw new Exception("Todo list not found");
            }
        }

        public async Task<bool> DeleteListAsync(long listId)
        {
            return await _repository.DeleteAsync(listId);
        }
    }
}
