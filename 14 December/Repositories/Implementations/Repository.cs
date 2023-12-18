using System.Linq.Expressions;
using _14_December.DAL;
using _14_December.Entities;
using _14_December.Entities.Base;
using _14_December.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _14_December.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : BaseEntity, new()
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;

        public Repository(AppDbContext context)
        {
                _context = context;
                _table = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
           await _context.AddAsync(entity);
        }

        public  void DeleteAsync(T entity)
        {
             _table.Remove(entity);
        }

        public IQueryable<T> GetAllAsync(
            Expression<Func<T, bool>>? expression = null,
            Expression<Func<T, object>>? orderExpression = null, 
            bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking =true,
            params string[] includes)
        {
            var query = _table.AsQueryable();
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (orderExpression is not null)
            {
                if (isDescending==true)
                {
                    query = query.OrderBy(orderExpression);
                }
                else
                {
                    query = query.OrderByDescending(orderExpression);
                }
            }
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query.Include(includes[i]);
                }
            }
            if (skip != 0) {  query = query.Skip(skip); }
            if (take != 0) { query.Take(take); }
            
            return isTracking?query:query.AsNoTracking();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            T entity = await _table.FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }
    }
}
