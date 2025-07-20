using System;
using System.Linq.Expressions;

namespace EmployeeService.Repositories;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<T> GetByIdAsync(long id, params Expression<Func<T, object>>[] includes);

    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
    Task<T> SingleOrDefaultAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> enties);
    void Update(T entity);
    void Remove(T entity);
    Task SoftRemove(long id);
    void RemoveRange(IEnumerable<T> entities);
}
