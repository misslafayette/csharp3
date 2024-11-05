namespace ToDoList.Test;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;

public class PostUnitTests
{
    [Fact]
    public void Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var repository = Substitute.For<IRepository<ToDoItem>>();
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var controller = new ToDoItemsController(repository);
        var request = new ToDoItemCreateRequestDto(Name: "Meno", Description: "Popis", IsCompleted: false);

        // Act
        var result = controller.Create(request);
        var resultResult = result.Result;
        var value = result.GetValue();

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
    }
}
