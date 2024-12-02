namespace ToDoList.Test.IntegrationTests;

using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class PostTests
{
    [Fact]
    public async Task Post_ValidRequest_ReturnsNewItem()
    {
        // Arrange
        var context = new ToDoItemsContext("Data Source=../../../../../data/localdb.db");
        var repository = new ToDoItemsRepository(context);
        var controller = new ToDoItemsController(repository);
        var request = new ToDoItemCreateRequestDto(
            Name: "Jmeno",
            Description: "Popis",
            IsCompleted: false,
            Category: "Kategória"
        );

        // Act
        var result = await controller.CreateAsync(request);
        Assert.NotNull(result); // temporary

        var resultResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        Assert.NotNull(resultResult.Value); //temporary

        // Assert
        var value = Assert.IsType<ToDoItemGetResponseDto>(result.Value);
        Assert.NotNull(value);

        Assert.Equal(request.Description, value.Description);
        Assert.Equal(request.IsCompleted, value.IsCompleted);
        Assert.Equal(request.Name, value.Name);
        Assert.Equal(request.Category, value.Category);
    }
}
