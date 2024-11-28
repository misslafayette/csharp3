namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class UpdateUnitTests
{
    [Fact]
    public async Task Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItemId = 1;
        var existingItem = new ToDoItem
        {
            ToDoItemId = toDoItemId,
            Name = "TestName",
            Description = "TestDescription",
            IsCompleted = false,
            Category = null
        };

        var updateRequest = new ToDoItemUpdateRequestDto
        (
            Name: "Updated Name",
            Description: "UpdatedDescription",
            IsCompleted: true,
            Category: "UpdatedCategory"
        );

        repositoryMock.ReadByIdAsync(toDoItemId).Returns(existingItem);

        // Act
        var result = controller.UpdateByIdAsync(toDoItemId, updateRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadByIdAsync(Arg.Any<int>()).Returns((ToDoItem)null);

        var updateRequest = new ToDoItemUpdateRequestDto
        (
            Name: "Updated Name",
            Description: "UpdatedDescription",
            IsCompleted: true,
            Category: "UpdatedCategory"
        );

        // Act
        var result = controller.UpdateByIdAsync(1, updateRequest);
        // var resultResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadByIdAsync(1);
        repositoryMock.DidNotReceive().UpdateAsync(Arg.Any<ToDoItem>());
    }

    [Fact]
    public async Task Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
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

        repositoryMock.ReadByIdAsync(toDoItem.ToDoItemId).Returns(toDoItem);
        repositoryMock.When(r => r.UpdateAsync(Arg.Any<ToDoItem>())).Do(x => throw new Exception());;

        var updateRequest = new ToDoItemUpdateRequestDto
        (
            Name: "UpdatedName",
            Description: "UpdatedDescription",
            IsCompleted: true,
            Category: "UpdatedCategory"
        );

        // Act
        var result = controller.UpdateByIdAsync(toDoItem.ToDoItemId, updateRequest);

        // Assert
        Assert.IsType<ObjectResult>(result);
        repositoryMock.Received(1).ReadByIdAsync(toDoItem.ToDoItemId);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
    }
}
