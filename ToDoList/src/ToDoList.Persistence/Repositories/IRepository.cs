namespace ToDoList.Persistence.Repositories;

public interface IRepository<T> where T : class
{
    public void Create(T item);
    IEnumerable<T> ReadAll();
    T ReadById(int id);
    public void Update(T item);
    public void Delete (int id);
}
