namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class CreateTests
{
    [Fact]
    public void Create_ValidItem_ReturnsCreatedItem()
    {

        // Arrange
        var controller = new ToDoItemsController();
        var newToDoItem = new ToDoItemCreateRequestDto("New Task", "New Task Description", false);

        // Act
        var result = controller.Create(newToDoItem);

        // Assert
        var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var item = Assert.IsType<ToDoItemGetResponseDto>(createdResult.Value);
        Assert.Equal("New Task", item.Name);
        Assert.Equal("New Task Description", item.Description);
        Assert.False(item.IsCompleted);
    }
}
