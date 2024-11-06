namespace ToDoList.Persistence.Repositories;

using ToDoList.Domain.Models;

public class ToDoItemsRepository : IRepository<ToDoItem>
{
    private readonly ToDoItemsContext context;

    public ToDoItemsRepository(ToDoItemsContext context)
    {
        this.context = context;
    }

    public void Create(ToDoItem item)
    {
        context.ToDoItems.Add(item);
        context.SaveChanges();
    }
    public IEnumerable<ToDoItem> ReadAll()
    {
        return context.ToDoItems.ToList();
    }

    public ToDoItem? ReadById(int id)
    {
        return context.ToDoItems.Find(id);
    }

    public void Update(ToDoItem item)
    {
        context.ToDoItems.Update(item);
        context.SaveChanges();
    }

    public void Delete(int id)
    {
        var itemToDelete = context.ToDoItems.Find(id);
        context.ToDoItems.Remove(itemToDelete);
        context.SaveChanges();
    }
}
