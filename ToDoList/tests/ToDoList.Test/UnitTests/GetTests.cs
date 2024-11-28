namespace ToDoList.Test.UnitTests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using ToDoList.Domain.Models;
using ToDoList.Persistence.Repositories;
using ToDoList.WebApi.Controllers;

public class GetUnitTests
{
    [Fact]
    public async Task Get_ReadAllAndSomeItemIsAvailable_ReturnsOk()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);

        // repositoryMock.When().Do();  // generické keď-tak
        // repositoryMock.ReadAll().Returns();  // nastavujem return value
        // repositoryMock.ReadAll().Throws();   // vyhadzujem výnimku
        // repositoryMock.Received().ReadAll();   // kontrolujem zavolanie metody

        repositoryMock.ReadAllAsync().Returns(
            [
                new ToDoItem{
                    Name = "testName",
                    Description = "testDescription",
                    IsCompleted = false,
                    Category = "testCategory"
                }
            ]
            );

        // Act
        var result = await controller.ReadAsync();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<OkObjectResult>(resultResult);
        await repositoryMock.Received(1).ReadAllAsync();
    }

    [Fact]
    public async Task Get_ReadAllExceptionOccured_ReturnInternalServerError()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAllAsync().Throws(new Exception());

        // Act
        var result = await controller.ReadAsync();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<ObjectResult>(resultResult);
        await repositoryMock.Received(1).ReadAllAsync();
        Assert.Equivalent(new StatusCodeResult(StatusCodes.Status500InternalServerError), resultResult);
    }

    [Fact]
    public async Task Get_ReadAllNoItemAvailable_ReturnsNotFound()
    {
        // Arrange
        var repositoryMock = Substitute.For<IRepositoryAsync<ToDoItem>>();
        var controller = new ToDoItemsController(repositoryMock);
        repositoryMock.ReadAllAsync().ReturnsNull();

        // Act
        var result = await controller.ReadAsync();
        var resultResult = result.Result;

        // Assert
        Assert.IsType<NotFoundResult>(resultResult);
        await repositoryMock.Received(1).ReadAllAsync();
    }
}
