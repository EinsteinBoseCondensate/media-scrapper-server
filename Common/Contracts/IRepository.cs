using System.Linq.Expressions;

namespace Common.Contracts;
public interface IRepository<T> where T : IEntity
{
    Task CreateAsync(T entity);
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter);
    Task<T> GetAsync(Guid id);
    Task<T> GetAsync(Expression<Func<T, bool>> filter);
    Task<long> CountAsync(Expression<Func<T, bool>> filter);
    Task RemoveAsync(Guid id);
    Task UpdateAsync(T entity);
}
