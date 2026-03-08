using EraasoftAcademy.DataAccess;
using EraasoftAcademy.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;

namespace EraasoftAcademy.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            var entities = _dbSet.AsQueryable();

            if (expression != null)
            {
                entities = entities.Where(expression);
            }
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    entities = entities.Include(include);
                }
            }
            if (!tracked)
            {
                entities = entities.AsNoTracking();
            }
            return await entities.ToListAsync();
        }
        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> expression, Expression<Func<T, object>>[]? includes = null, bool tracked = true)
        {
            return (await GetAllAsync(expression, includes, tracked)).FirstOrDefault();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
