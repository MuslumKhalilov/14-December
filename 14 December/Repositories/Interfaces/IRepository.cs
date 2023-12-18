using System.Data.SqlTypes;
using System.Linq.Expressions;
using _14_December.Entities;
using _14_December.Entities.Base;

namespace _14_December.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T,bool>>? expression= null, params string[] includes);
        Task<T> GetByIDAsync(int id);
        Task AddAsync(T category);
        void DeleteAsync(T category);
        void UpdateAsync(T category);
        Task SaveChangesAsync();
    }
}
