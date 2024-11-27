namespace ToDoList.Persistence.Repositories;

public interface IRepositoryAsync<T> where T : class
{
    Task CreateAsync(T item);
    Task<IEnumerable<T>> ReadAllAsync();
    Task<T?> ReadByIdAsync(int id);
    Task UpdateAsync(T item);
    Task DeleteAsync (int id);
}
