namespace ToDoList.WebApi.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;
using ToDoList.Persistence;
using ToDoList.Persistence.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public static readonly List<ToDoItem> items = []; //uz zde nechceme pracovat se statickym listem, muzeme smazat

    private readonly ToDoItemsContext context; //uz zde nechceme pracovat s context, na to mame repository, muzeme smazat
    private readonly IRepository<ToDoItem> repository;

    //staci nam pouze ten druhy konstruktor, uz nechceme tady nechceme pracovat s context
    //zaroven pokud ted mame 2 konstruktory tak pokud spustime nasi web API a posleme nejaky HTTP request, tak nam vyskoci exception
    /*public ToDoItemsController(ToDoItemsContext context, IRepository<ToDoItem> repository)
    {
        this.context = context;
        this.repository = repository;
    }
*/
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
            //kazdy ukol se prida dvakrat, nevim jak ty, ale cim mene ukolu tim lepe a jeste lepsi kdyz je nemam duplikovane :)
            repository.Create(item);
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
            var readList = context.ToDoItems.ToList(); //proc tady pracujeme s context? chceme pracovat s repository ne? Cilem bylo se toho zbavit :)
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
        try
        {
            var itemToGet = repository.ReadById(toDoItemId);

            return (itemToGet is null)
            ? NotFound()
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet));
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var itemToUpdate = repository.ReadById(toDoItemId);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = request.Name;
            itemToUpdate.Description = request.Description;
            itemToUpdate.IsCompleted = request.IsCompleted;

            repository.Update(itemToUpdate);
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
            var itemToDelete = repository.ReadById(toDoItemId);

            if (itemToDelete is null)
            {
                return NotFound();
            }

            repository.Delete(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent();
    }
}
