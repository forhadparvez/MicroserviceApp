using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
                query = query.Include(include);
        }
        query = ApplyDeleteFilter(query);
        return await query.ToListAsync();
    }

    public async Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
                query = query.Include(include);
        }
        query = ApplyDeleteFilter(query);
        // Assuming entity has a key named "Id" of type int
        return await query.FirstOrDefaultAsync(e => EF.Property<long>(e, "Id") == id);
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(predicate);

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
                query = query.Include(include);
        }
        query = ApplyDeleteFilter(query);
        return await query.ToListAsync();
    }

    public async Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet.Where(predicate);

        if (includes != null && includes.Any())
        {
            foreach (var include in includes)
                query = query.Include(include);
        }
        query = ApplyDeleteFilter(query);
        return await query.SingleOrDefaultAsync();
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);

    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Remove(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task SoftRemove(long id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            var prop = typeof(T).GetProperty("IsDeleted");
            if (prop != null)
            {
                prop.SetValue(entity, true);
                _dbSet.Update(entity);
            }
        }
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    private IQueryable<T> ApplyDeleteFilter(IQueryable<T> query)
    {
        var prop = typeof(T).GetProperty("IsDeleted");
        if (prop != null && prop.PropertyType == typeof(bool))
        {
            var parameter = Expression.Parameter(typeof(T), "e");
            var propertyAccess = Expression.Call(
                typeof(EF),
                nameof(EF.Property),
                new Type[] { typeof(bool) },
                parameter,
                Expression.Constant("IsDeleted")
            );
            var condition = Expression.Equal(propertyAccess, Expression.Constant(false));  // IsDeleted == false
            var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            query = query.Where(lambda);
        }

        return query;
    }

}
