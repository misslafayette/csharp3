namespace ToDoList.Test.UnitTests;

using NSubstitute;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.WebApi.Controllers;
using ToDoList.Persistence.Repositories;
using ToDoList.Domain.Models;
using Microsoft.AspNetCore.Http;

public class DeleteByIdUnitTests
{
    [Fact]
    public async Task Delete_ValidItemId_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            Name = "testItem",
            Description = "testDescription",
            IsCompleted = false,
            ToDoItemId = 1,
            Category = "testCategory"
        };

        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns(toDoItem);

        // Act
        var result = await controller.DeleteByIdAsync(toDoItem.ToDoItemId);

        // Assert
        Assert.IsType<NoContentResult>(result);
        await repositoryMock.Received(1).DeleteAsync(toDoItem.ToDoItemId);
    }

    [Fact]
    public async Task Delete_DeleteByIdInvalidItemId_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns((ToDoItem)null);

        // Act
        var result = await controller.DeleteByIdAsync(1);
        var resultResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        await repositoryMock.Received(1).ReadByIdAsync(1);
        await repositoryMock.DidNotReceive().DeleteAsync(Arg.Any<int>());
    }
}
