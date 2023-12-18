using System.Linq.Expressions;
using _14_December.DAL;
using _14_December.Entities;
using _14_December.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _14_December.Repositories.Implementations
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
                _context = context;
        }
        public async Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category, bool>>? expression = null, params string[] includes)
        {
            var query = _context.Categories.AsQueryable();
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            if (includes != null)
            {
                for (int i = 0; i < includes.Length; i++)
                {
                    query.Include(includes[i]);
                }
            }
            return query;
        }

        public async Task<Category> GetByIDAsync(int id)
        {
            Category category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            return category;
        }
    }
}
