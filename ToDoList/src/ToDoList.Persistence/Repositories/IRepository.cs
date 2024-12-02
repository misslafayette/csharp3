namespace ToDoList.Persistence.Repositories;

public interface IRepositoryAsync<T> where T : class //chtelo by to aby se trida nazivala stejne jako soubor, cili sobour bych prejmenoval na IRepositoryAsync.cs
{
    Task CreateAsync(T item);
    Task<IEnumerable<T>> ReadAllAsync();
    Task<T?> ReadByIdAsync(int id);
    Task UpdateAsync(T item);
    Task DeleteAsync(int id);
}
