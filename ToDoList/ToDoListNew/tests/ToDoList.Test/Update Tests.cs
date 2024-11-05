namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class UpdateTests
{
    [Fact]
    public void UpdateById_ExistingId_UpdatesItem()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var existingItem = new ToDoItem { ToDoItemId = 1, Name = "Old Task", Description = "Old Description", IsCompleted = false }; ToDoItemsController.items.Add(existingItem);
        var updatedItem = new ToDoItemUpdateRequestDto("Updated Task", "Updated Description", true);

        // Act
        IActionResult result = controller.UpdateById(1, updatedItem);

        // Assert
        Assert.IsType<NoContentResult>(result);
        Assert.Equal("Updated Task", ToDoItemsController.items[0].Name);
        Assert.Equal("Updated Description", ToDoItemsController.items[0].Description);
        Assert.True(ToDoItemsController.items[0].IsCompleted);
    }

    [Fact]
    public void UpdateById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var updatedItemDto = new ToDoItemUpdateRequestDto("Updated Task", "Updated Description", true);

        // Act
        var result = controller.UpdateById(999, updatedItemDto);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }
}
