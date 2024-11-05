namespace ToDoList.Test;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.WebApi.Controllers;

public class CreateTests
{
    //skvele, slo by udelat ale jako parametrizovatelny test - at to otestuje napr ze do defaultne nedava isCompleted na false bez ohledu co zadame
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
        Assert.Equal("New Task", item.Name); //bylo by lepsi to porovnat pres newToDoItem.Name at neni "New Task" hadrcoded do assertu
        Assert.Equal("New Task Description", item.Description); //stejne
        Assert.False(item.IsCompleted); //lepsi by bylo porovnat newToDoItem.IsCompleted
    }
}
