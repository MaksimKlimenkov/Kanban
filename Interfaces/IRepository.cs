namespace Kanban.Interfaces;

public interface IRepository<T> where T : class
{
    public Task<T> CreateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task SaveAsync();
}