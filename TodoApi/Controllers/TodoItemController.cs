using Hangfire;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/todolists/{listId}/todoitems")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoItemController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItemsByListId(long listId)
        {
            var todoItems = await _todoService.GetAllItemsByListIdAsync(listId);
            if (!todoItems.Any())
            {
                return NotFound(new { Message = $"No items found for TodoList with ID {listId}." });
            }
            return Ok(todoItems);
        }

        [HttpGet("{itemId}")]
        public async Task<ActionResult<TodoItem>> GetTodoItemByList(long listId, long itemId)
        {
            var todoItem = await _todoService.GetItemByIdAsync(listId, itemId);
            if (todoItem == null)
            {
                return NotFound(
                    new { Message = $"TodoItem with ID {itemId} not found in TodoList {listId}." }
                );
            }
            return Ok(todoItem);
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(long listId, CreateTodoItem item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest(new { Message = "Invalid item data. Name is required." });
            }

            var todoItem = await _todoService.CreateItemAsync(listId, item);
            return CreatedAtAction(
                nameof(GetTodoItemByList),
                new { listId = listId, itemId = todoItem.Id },
                todoItem
            );
        }

        [HttpPut("{itemId}")]
        public async Task<ActionResult<TodoItem>> PutTodoItem(
            long listId,
            long itemId,
            UpdateTodoItem item
        )
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Name))
            {
                return BadRequest(new { Message = "Invalid item data. Name is required." });
            }

            var todoItem = await _todoService.UpdateItemAsync(itemId, item);
            if (todoItem == null)
            {
                return NotFound(
                    new { Message = $"TodoItem with ID {itemId} not found in TodoList {listId}." }
                );
            }

            return Ok(todoItem);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult> DeleteTodoItem(long listId, long itemId)
        {
            var success = await _todoService.DeleteItemAsync(itemId);
            if (!success)
            {
                return NotFound(
                    new { Message = $"TodoItem with ID {itemId} not found in TodoList {listId}." }
                );
            }
            return NoContent();
        }

        [HttpDelete("deleteall")]
        public IActionResult DeleteAllTodoItems(long listId)
        {
            BackgroundJob.Enqueue(() => _todoService.DeleteAllItemsByListIdAsync(listId));
            return Accepted(
                new { Message = $"Deletion job for TodoList {listId} has been enqueued." }
            );
        }
    }
}
