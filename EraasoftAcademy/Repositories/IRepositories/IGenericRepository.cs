using EraasoftAcademy.Models;
using System.Linq.Expressions;

namespace EraasoftAcademy.Repositories.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<T> GetByIdAsync(int id, Expression<Func<T, object>>[]? includes = null);
        Task<Quiz> GetByIdAsync_2(int id, Func<IQueryable<Quiz>, IQueryable<Quiz>> include = null);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true);
        Task<T?> GetOneAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null, bool tracked = true);
        Task SaveChangesAsync();
    }
}
