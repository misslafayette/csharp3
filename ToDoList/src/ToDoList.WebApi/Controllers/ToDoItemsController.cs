namespace ToDoList.WebApi.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public static readonly List<ToDoItem> items = [];

    private readonly ToDoItemsContext context;
    private readonly IRepository<ToDoItem> repository;
    public ToDoItemsController(ToDoItemsContext context, IRepository<ToDoItem> repository)
    {
        this.context = context;
        this.repository = repository;
    }

    public ToDoItemsController(IRepository<ToDoItem> repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            repository.Create(item);
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        try
        {
            var readList = context.ToDoItems.ToList();
            return (readList is null)
            ? NotFound()
            : Ok(readList.Select(ToDoItemGetResponseDto.FromDomain));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        ToDoItem? itemToGet;
        try
        {
            itemToGet = context.ToDoItems.Find(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return (itemToGet is null)
            ? NotFound()
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        var updatedItem = request.ToDomain();

        try
        {
            var itemToUpdate = context.ToDoItems.Find(toDoItemId);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = request.Name;
            itemToUpdate.Description = request.Description;
            itemToUpdate.IsCompleted = request.IsCompleted;
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        try
        {
            var itemToDelete = context.ToDoItems.Find(toDoItemId);

            if (itemToDelete is null)
            {
                return NotFound();
            }
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent();
    }
}
