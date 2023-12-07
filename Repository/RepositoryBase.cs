using Kanban.Data;
using Kanban.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kanban.Repository;

public abstract class RepositoryBase<T> : IRepository<T> where T : class
{
    private readonly ApplicationContext _context;
    public IQueryable<T> Query { get; }
    protected DbSet<T> Table { get; }

    protected RepositoryBase(ApplicationContext context)
    {
        var props = context.GetType().GetProperties();
        var prop = props.FirstOrDefault(p => p.PropertyType == typeof(DbSet<T>));
        if (prop == null)
            throw new Exception($"DbSet<{typeof(T).Name}> not found in context");
        var table = (DbSet<T>)prop.GetValue(context)!;
        _context = context;
        Table = table;
        Query = Table.AsQueryable();
    }

    public async Task<T?> FindByIdAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        return entity;
    }

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
        await _context.SaveChangesAsync();
    }
}