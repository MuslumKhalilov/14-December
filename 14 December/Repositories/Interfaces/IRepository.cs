using System.Data.SqlTypes;
using System.Linq.Expressions;
using _14_December.Entities;

namespace _14_December.Repositories.Interfaces
{
    public interface IRepository
    {
        Task<IQueryable<Category>> GetAllAsync(Expression<Func<Category,bool>>? expression= null, params string[] includes);
        Task<Category> GetByIDAsync(int id);
        Task AddAsync(Category category);
        void DeleteAsync(Category category);
        void UpdateAsync(Category category);
        Task SaveChangesAsync();
    }
}
