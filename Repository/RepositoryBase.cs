using Kanban.Data;
using Kanban.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    protected readonly ApplicationContext Context;
    public IQueryable<T> Query = null!;
    protected DbSet<T> Table = null!;
    protected RepositoryBase(ApplicationContext context) => Context = context;
    
    public virtual async Task<T> CreateAsync(T entity)
    {
        var newEntity = await Table.AddAsync(entity);
        await SaveAsync();
        return newEntity.Entity;
    }

    public virtual async Task DeleteAsync(T entity)
    {
        Table.Remove(entity);
        await SaveAsync();
    }

    public virtual async Task SaveAsync()
    {
        await Context.SaveChangesAsync();
    }
}