namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetByIdUnitTests
{
    [Fact]
    public void Get_ReadByIdAndSomeItemIsAvailable_ReturnsOk()
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

        repositoryMock.ReadById(Arg.Any<int>()).Returns(toDoItem);

        // Act
        var result = controller.Read();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        repositoryMock.Received(1).ReadAll();
    }

    [Fact]
    public void Get_ReadByIdUnhandledException_ReturnsInternalServerError()
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

        repositoryMock.ReadById(Arg.Any<int>()).Throws(new Exception());

        // Act
        var result = controller.ReadById(toDoItem.ToDoItemId);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        repositoryMock.Received(1).ReadById(1);
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }

    [Fact]
    public void Get_ReadByIdNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepository<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadById(Arg.Any<int>()).Returns((ToDoItem)null);

        // Act
        var result = controller.ReadById(1);
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        repositoryMock.Received(1).ReadById(1);
    }
}
