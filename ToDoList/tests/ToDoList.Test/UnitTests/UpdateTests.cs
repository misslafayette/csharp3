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
    public void Put_UpdateByIdWhenItemUpdated_ReturnsNoContent()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItemId = 1;
        var existingItem = new ToDoItem
        {
            ToDoItemId = toDoItemId,
            Name = "TestName",
            Description = "TestDescription",
            IsCompleted = false
        };

        var updateRequest = new ToDoItemUpdateRequestDto
        (
            Name: "Updated Name",
            Description: "UpdatedDescription",
            IsCompleted: true
        );

        repositoryMock.ReadById(toDoItemId).Returns(existingItem);

        // Act
        var result = controller.UpdateById(toDoItemId, updateRequest);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public void Put_UpdateByIdWhenIdNotFound_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Returns((ToDoItem)null);

        var updateRequest = new ToDoItemUpdateRequestDto
        (
            Name: "Updated Name",
            Description: "UpdatedDescription",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(1, updateRequest);
        // var resultResult = result as NotFoundResult;

        // Assert
        Assert.IsType<NotFoundResult>(result);
        repositoryMock.Received(1).ReadById(1);
        repositoryMock.DidNotReceive().Update(Arg.Any<ToDoItem>());
    }

    [Fact]
    public void Put_UpdateByIdUnhandledException_ReturnsInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        var toDoItem = new ToDoItem
        {
            Name = "testItem",
            Description = "testDescription",
            IsCompleted = false,
            ToDoItemId = 1
        };

        repositoryMock.ReadById(toDoItem.ToDoItemId).Returns(toDoItem);
        repositoryMock.When(r => r.Update(Arg.Any<ToDoItem>())).Do(x => throw new Exception());;

        var updateRequest = new ToDoItemUpdateRequestDto
        (
            Name: "Updated Name",
            Description: "UpdatedDescription",
            IsCompleted: true
        );

        // Act
        var result = controller.UpdateById(toDoItem.ToDoItemId, updateRequest);

        // Assert
        Assert.IsType<ObjectResult>(result);
        repositoryMock.Received(1).ReadById(toDoItem.ToDoItemId);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), result);
    }
}
