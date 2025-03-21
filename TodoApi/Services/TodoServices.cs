using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Dtos;
using TodoApi.Models;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;

        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TodoItem>> GetAllItemsByListIdAsync(long listId)
        {
            return await _repository.GetAllAsync(listId);
        }

        public async Task<TodoItem> GetItemByIdAsync(long listId, long itemId)
        {
            return await _repository.GetByIdAsync(listId, itemId);
        }

        public async Task<TodoItem> CreateItemAsync(long listId, CreateTodoItem item)
        {
            var todoItem = await _repository.AddAsync(listId, item);
            return todoItem;
        }

        public async Task<TodoItem> UpdateItemAsync(long itemId, UpdateTodoItem item)
        {
            var todoItem = await _repository.UpdateAsync(itemId, item);
            return todoItem;
        }

        public async Task<bool> DeleteItemAsync(long itemId)
        {
            await _repository.DeleteAsync(itemId);
            return true;
        }

        public async Task DeleteAllItemsByListIdAsync(long listId)
        {
            await _repository.DeleteAllAsync(listId);
        }

        
    }
}
