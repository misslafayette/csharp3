namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class DeleteTests
{
    [Fact]
    public void DeleteById_ExistingId_DeletesItem()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem { ToDoItemId = 1, Name = "Task to Delete" };
        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.DeleteById(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Empty(ToDoItemsController.items);
    }

    [Fact]
    public void DeleteById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var controller = new ToDoItemsController();

        // Act
        var result = controller.DeleteById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
