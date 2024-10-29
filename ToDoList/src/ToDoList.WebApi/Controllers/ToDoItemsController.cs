namespace ToDoList.WebApi.Controllers;

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Domain.DTOs;
using ToDoList.Domain.Models;
using ToDoList.Persistence;

[ApiController]
[Route("api/[controller]")]
public class ToDoItemsController : ControllerBase
{
    public static readonly List<ToDoItem> items = [];

    private readonly ToDoItemsContext context;
    public ToDoItemsController(ToDoItemsContext context)
    {
        this.context = context;
    }

    [HttpPost]
    public ActionResult<ToDoItemGetResponseDto> Create(ToDoItemCreateRequestDto request)
    {
        //map to Domain object as soon as possible
        var item = request.ToDomain();

        //try to create an item
        try
        {
            // item.ToDoItemId = items.Count == 0 ? 1 : items.Max(o => o.ToDoItemId) + 1;
            // items.Add(item);
            context.ToDoItems.Add(item);
            context.SaveChanges();
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return CreatedAtAction(
            nameof(ReadById),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item)); //201
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDoItemGetResponseDto>> Read()
    {
        List<ToDoItem> itemsToGet;
        try
        {
            var readList = context.ToDoItems.ToList();
            // itemsToGet = items;

            //respond to client
            return (readList is null)
            ? NotFound() //404
            : Ok(readList.Select(ToDoItemGetResponseDto.FromDomain)); //200

            //tento kod bol až pod catch, ale kričalo to na mňa, že readList neexistuje v tom kontexte.
            // skusala som ten var readList iniciovať pred tým try, ale potrebovalo to hodnotu,
            // a var mi tam ako null nechcelo nijako zobrať. trošku som sa v tom pohrabkala online,
            // a zistila som, že by som to mohla iniciovať napríklad ako object readList = null;
            // ale tie return funkcie sa potom nekamarátili s tým object, tak som nakoniec presunula
            // iba tento return co bol pod catch do tohto try - je to tak v pohode? vyriešilo mi to
            // tie errory čo tu na mňa vyskakovali, ale neviem či je tá logika za tým dobrá
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }
    }

    [HttpGet("{toDoItemId:int}")]
    public ActionResult<ToDoItemGetResponseDto> ReadById(int toDoItemId)
    {
        //try to retrieve the item by id
        ToDoItem? itemToGet;
        try
        {
            // itemToGet = items.Find(i => i.ToDoItemId == toDoItemId);
            itemToGet = context.ToDoItems.Find(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return (itemToGet is null)
            ? NotFound() //404
            : Ok(ToDoItemGetResponseDto.FromDomain(itemToGet)); //200
    }

    [HttpPut("{toDoItemId:int}")]
    public IActionResult UpdateById(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        //map to Domain object as soon as possible
        var updatedItem = request.ToDomain();

        //try to update the item by retrieving it with given id
        try
        {
            // retrieve the item
            // var itemIndexToUpdate = items.FindIndex(i => i.ToDoItemId == toDoItemId);
            var itemToUpdate = context.ToDoItems.Find(toDoItemId);

            if (itemToUpdate == null)
            {
            return NotFound(); // 404
            }

            //updatedItem.ToDoItemId = toDoItemId;
            //items[itemIndexToUpdate] = updatedItem;

            itemToUpdate.Name = request.Name;
            itemToUpdate.Description = request.Description;
            itemToUpdate.IsCompleted = request.IsCompleted;
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError); //500
        }

        //respond to client
        return NoContent(); //204
    }

    [HttpDelete("{toDoItemId:int}")]
    public IActionResult DeleteById(int toDoItemId)
    {
        //try to delete the item
        try
        {
            //var itemToDelete = items.Find(i => i.ToDoItemId == toDoItemId);
            var itemToDelete = context.ToDoItems.Find(toDoItemId);

            if (itemToDelete is null)
            {
                return NotFound(); //404
            }
            // items.Remove(itemToDelete);
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        //respond to client
        return NoContent(); //204
    }
}
