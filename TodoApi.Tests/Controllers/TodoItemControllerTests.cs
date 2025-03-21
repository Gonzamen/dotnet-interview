using System.Threading.Tasks;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Controllers;
using TodoApi.Models;
using TodoApi.Services.TodoItem;
using TodoApi.Repositories.TodoItem;

namespace TodoApi.Tests.Controllers;

public class TodoItemControllerTests
{
    public TodoItemControllerTests()
    {
        GlobalConfiguration.Configuration.UseMemoryStorage();
    }

    private DbContextOptions<TodoContext> DatabaseContextOptions()
    {
        return new DbContextOptionsBuilder<TodoContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
    }

    private void PopulateDatabaseContext(TodoContext context)
    {
        context.TodoList.Add(new TodoList { Id = 1, Name = "Task 1" });
        context.TodoList.Add(new TodoList { Id = 2, Name = "Task 2" });
        context.TodoItem.Add(
            new TodoItem
            {
                Id = 1,
                Name = "Item 1",
                ListId = 1,
            }
        );
        context.TodoItem.Add(
            new TodoItem
            {
                Id = 2,
                Name = "Item 2",
                ListId = 1,
            }
        );
        context.TodoItem.Add(
            new TodoItem
            {
                Id = 3,
                Name = "Item 3",
                ListId = 2,
            }
        );
        context.SaveChanges();
    }

    [Fact]
    public async Task GetTodoItemByList_WhenCalled_ReturnsTodoItem()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        {
            PopulateDatabaseContext(context);

            var todoService = new TodoItemService(new TodoItemRepository(context));
            var controller = new TodoItemController(todoService);

            var result = await controller.GetTodoItemByList(1, 1);
            Assert.Equal(1, ((result.Result as OkObjectResult).Value as TodoItem).Id);
            Assert.Equal(1, ((result.Result as OkObjectResult).Value as TodoItem).ListId);
        }
    }

    [Fact]
    public async Task GetTodoItemByList_WhenCalled_ReturnsNotFound()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        {
            PopulateDatabaseContext(context);
            var todoService = new TodoItemService(new TodoItemRepository(context));
            var controller = new TodoItemController(todoService);
            var result = await controller.GetTodoItemByList(1, 4);
            Assert.IsType<NotFoundObjectResult>(result.Result);
        }
    }

    [Fact]
    public async Task PostTodoItem_WhenCalled_CreatesTodoItem()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        {
            PopulateDatabaseContext(context);
            var todoService = new TodoItemService(new TodoItemRepository(context));
            var controller = new TodoItemController(todoService);
            var result = await controller.PostTodoItem(
                1,
                new Dtos.CreateTodoItem { Name = "Item 4" }
            );

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(4, context.TodoItem.Count());
        }
    }

    [Fact]
    public async Task PutTodoItem_WhenCalled_UpdatesTodoItem()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        {
            PopulateDatabaseContext(context);
            var todoService = new TodoItemService(new TodoItemRepository(context));
            var controller = new TodoItemController(todoService);
            var result = await controller.PutTodoItem(
                1,
                1,
                new Dtos.UpdateTodoItem { Name = "Item 1 Updated", IsComplete = true }
            );
            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal("Item 1 Updated", context.TodoItem.Find(1L).Name);
        }
    }

    [Fact]
    public async Task DeleteTodoItem_WhenCalled_DeletesTodoItem()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        {
            PopulateDatabaseContext(context);
            var todoService = new TodoItemService(new TodoItemRepository(context));
            var controller = new TodoItemController(todoService);
            var result = await controller.DeleteTodoItem(1, 1);
            Assert.IsType<NoContentResult>(result);
            Assert.Equal(2, context.TodoItem.Count());
        }
    }

    [Fact]
    public async Task DeleteAllTodoItems_WhenCalled_DeletesAllTodoItems()
    {
        using (var context = new TodoContext(DatabaseContextOptions()))
        {
            PopulateDatabaseContext(context);
            var todoService = new TodoItemService(new TodoItemRepository(context));
            var controller = new TodoItemController(todoService);
            var result = controller.DeleteAllTodoItems(1);
            Assert.IsType<AcceptedResult>(result);

            await todoService.DeleteAllItemsByListIdAsync(1);

            var todoItemsResult = await controller.GetTodoItemsByListId(1);
            Assert.IsType<NotFoundObjectResult>(todoItemsResult.Result);
        }
    }
}
