namespace ToDoList.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    private static List <ToDoItem> items = [];

    [HttpPost]
    public IActionResult Create(ToDoItemCreateRequestDto request)
    {
        return Ok();
    }

    [HttpGet]
    public IActionResult Read()
    {

        try
        {
            throw new Exception("Nieco sa pokazilo.");
        }
        catch(Exception ex)
        {
            this.Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return Ok();
    }

    [HttpGet("{toDoItemId: int}")]
    public IActionResult ReadById(int toDoItemId)
    {
        return Ok();
    }

    [HttpPut("{toDoItemId: int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpadeRequestDto request)
    {
        return Ok();
    }

    [HttpDelete("{toDoItemId: int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        return Ok();
    }
}
