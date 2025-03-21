using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;

        public TodoRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync(long listId)
        {
            return await _context.TodoItem.Where(item => item.ListId == listId).ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync(long listId, long itemId)
        {
            return await _context
                .TodoItem.Where(item => item.ListId == listId && item.Id == itemId)
                .FirstOrDefaultAsync();
        }

        public async Task<TodoItem> AddAsync(long listId, CreateTodoItem item)
        {
            var todoItem = new TodoItem
            {
                ListId = listId,
                Name = item.Name,
                IsComplete = false,
            };
            await _context.TodoItem.AddAsync(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<TodoItem> UpdateAsync(long itemId, UpdateTodoItem item)
        {
            var todoItem = await _context.TodoItem.FindAsync(itemId);
            if (todoItem == null)
            {
                throw new Exception("Todo item not found");
            }
            todoItem.Name = item.Name;
            todoItem.IsComplete = item.IsComplete;
            _context.TodoItem.Update(todoItem);
            await _context.SaveChangesAsync();
            return todoItem;
        }

        public async Task<bool> DeleteAsync(long itemId)
        {
            var item = await _context.TodoItem.FindAsync(itemId);
            if (item != null)
            {
                _context.TodoItem.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task DeleteAllAsync(long listId)
        {
            var items = await _context.TodoItem.Where(item => item.ListId == listId).ToListAsync();
            _context.TodoItem.RemoveRange(items);
            await _context.SaveChangesAsync();
        }
    }
}
