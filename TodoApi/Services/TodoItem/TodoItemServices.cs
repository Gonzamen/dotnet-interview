using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Dtos;
using Models = TodoApi.Models;
using TodoApi.Repositories.TodoItem;

namespace TodoApi.Services.TodoItem
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ITodoItemRepository _repository;

        public TodoItemService(ITodoItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Models.TodoItem>> GetAllItemsByListIdAsync(long listId)
        {
            return await _repository.GetAllAsync(listId);
        }

        public async Task<Models.TodoItem> GetItemByIdAsync(long listId, long itemId)
        {
            return await _repository.GetByIdAsync(listId, itemId);
        }

        public async Task<Models.TodoItem> CreateItemAsync(long listId, CreateTodoItem item)
        {
            var todoItem = await _repository.AddAsync(listId, item);
            return todoItem;
        }

        public async Task<Models.TodoItem> UpdateItemAsync(long itemId, UpdateTodoItem item)
        {
            var todoItem = await _repository.UpdateAsync(itemId, item);
            return todoItem;
        }

        public async Task<bool> DeleteItemAsync(long itemId)
        {
            var isDeleted = await _repository.DeleteAsync(itemId);
            return isDeleted;
        }

        public async Task DeleteAllItemsByListIdAsync(long listId)
        {
            await _repository.DeleteAllAsync(listId);
        }
    }
}
