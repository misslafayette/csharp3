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
    private readonly IRepositoryAsync<ToDoItem> repository;
    public ToDoItemsController(IRepositoryAsync<ToDoItem> repository)
    {
        this.repository = repository;
    }

    [HttpPost]
    public async Task<ActionResult<ToDoItemGetResponseDto>> CreateAsync(ToDoItemCreateRequestDto request)
    {
        var item = request.ToDomain();
        try
        {
            await repository.CreateAsync(item);
        }

        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return CreatedAtAction(
            nameof(ReadByIdAsync),
            new { toDoItemId = item.ToDoItemId },
            ToDoItemGetResponseDto.FromDomain(item));
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToDoItemGetResponseDto>>> ReadAsync()
    {
        try
        {
            var readList = await repository.ReadAllAsync();
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
    public async Task<ActionResult<ToDoItemGetResponseDto>> ReadByIdAsync(int toDoItemId)
    {
        try
        {
            var itemToGet = await repository.ReadByIdAsync(toDoItemId);

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
    public async Task<IActionResult> UpdateByIdAsync(int toDoItemId, [FromBody] ToDoItemUpdateRequestDto request)
    {
        try
        {
            var itemToUpdate = await repository.ReadByIdAsync(toDoItemId);

            if (itemToUpdate == null)
            {
                return NotFound();
            }

            itemToUpdate.Name = request.Name;
            itemToUpdate.Description = request.Description;
            itemToUpdate.IsCompleted = request.IsCompleted;
            itemToUpdate.Category = request.Category;

            await repository.UpdateAsync(itemToUpdate);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }

        return NoContent();
    }

    [HttpDelete("{toDoItemId:int}")]
    public async Task<IActionResult> DeleteByIdAsync(int toDoItemId)
    {
        try
        {
            var itemToDelete = await repository.ReadByIdAsync(toDoItemId);

            if (itemToDelete is null)
            {
                return NotFound();
            }

            await repository.DeleteAsync(toDoItemId);
        }
        catch (Exception ex)
        {
            return Problem(ex.Message, null, StatusCodes.Status500InternalServerError);
        }
        return NoContent();
    }
}
