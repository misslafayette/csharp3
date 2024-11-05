namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class ReadByIdTests //nazev souboru je "Read By Id Test.cs" - radeji bez mezer :)
{
    [Fact]
    public void ReadById_ExistingId_ReturnsItem()
    {
        // Arrange
        var controller = new ToDoItemsController();
        var toDoItem = new ToDoItem { ToDoItemId = 1, Name = "Sample Task" };
        ToDoItemsController.items.Add(toDoItem);

        // Act
        var result = controller.ReadById(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var item = Assert.IsType<ToDoItemGetResponseDto>(okResult.Value);
        Assert.Equal(1, item.Id);
        Assert.Equal("Sample Task", item.Name); //opet radeji ne hardcoded, ale pres toDoItem.Name

    }

    [Fact]
    public void ReadById_NonExistingId_ReturnsNotFound()
    {
        // Arrange
        var controller = new ToDoItemsController();

        // Act
        var result = controller.ReadById(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
