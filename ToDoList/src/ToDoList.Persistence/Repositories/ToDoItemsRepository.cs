namespace ToDoList.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepositoryAsync<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public async Task CreateAsync(ToDoItem item)
    {
        await context.ToDoItems.AddAsync(item);
        await context.SaveChangesAsync();
    }
    public async Task<IEnumerable<ToDoItem>> ReadAllAsync()
    {
        return await context.ToDoItems.ToListAsync();
    }

    public async Task<ToDoItem?> ReadByIdAsync(int id)
    {
        return await context.ToDoItems.FindAsync(id);
    }

    public async Task UpdateAsync(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var itemToDelete = await context.ToDoItems.FindAsync(id);
        context.ToDoItems.Remove(itemToDelete);
        await context.SaveChangesAsync();
    }
}
