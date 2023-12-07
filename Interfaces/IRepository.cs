namespace Kanban.Interfaces;

public interface IRepository<T> where T : class
{
    public IQueryable<T> Query { get; }
    public Task<T?> FindByIdAsync(int id);
    public Task<T> CreateAsync(T entity);
    public Task DeleteAsync(T entity);
    public Task SaveAsync();
}