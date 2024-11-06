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
        if (itemToDelete != null) //tato kontrola uz probiha v kontroleru
        {
            context.ToDoItems.Remove(itemToDelete);
            context.SaveChanges();
        }

        //obecne nevadi zde provadet kontrolu zda muzeme smazat ukol, ALE kdyz se ted podivas na ostatni metody ToDoItemsRepository, tak je to lehce schizofreni :)
        //Update napriklad kontrolu neprovadi a Delete ano? Bud obe metody si tady to budou kontrolovat, anebo ani jedna a kontrola probehne v kontroleru.
    }
}
