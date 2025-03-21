using Microsoft.EntityFrameworkCore;
using TodoApi.Dtos;
using Models = TodoApi.Models;

namespace TodoApi.Repositories.TodoList
{
    public class TodoListRepository : ITodoListRepository
    {
        private readonly TodoContext _context;

        public TodoListRepository(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Models.TodoList>> GetAllAsync()
        {
            return await _context.TodoList.ToListAsync();
        }

        public async Task<Models.TodoList> GetByIdAsync(long listId)
        {
            var todoList = await _context.TodoList.FindAsync(listId);
            if (todoList == null)
            {
                throw new Exception("Todo list not found");
            }
            return todoList;
        }

        public async Task<Models.TodoList> CreateAsync(CreateTodoList list)
        {
            var todoList = new Models.TodoList { Name = list.Name };
            _context.TodoList.Add(todoList);
            await _context.SaveChangesAsync();
            return todoList;
        }

        public async Task<Models.TodoList> UpdateAsync(long listId, UpdateTodoList list)
        {
            var todoList = await _context.TodoList.FindAsync(listId);
            if (todoList == null)
            {
                throw new Exception("Todo list not found");
            }
            todoList.Name = list.Name;
            _context.TodoList.Update(todoList);
            await _context.SaveChangesAsync();
            return todoList;
        }

        public async Task<bool> DeleteAsync(long listId)
        {
            var todoList = await _context.TodoList.FindAsync(listId);
            if (todoList == null)
            {
                return false;
            }
            _context.TodoList.Remove(todoList);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
