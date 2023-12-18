using System.Data.SqlTypes;
using System.Linq.Expressions;
using _14_December.Entities;
using _14_December.Entities.Base;

namespace _14_December.Repositories.Interfaces
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<IQueryable<T>> GetAllAsync(
            Expression<Func<T,bool>>? expression= null,
            Expression<Func<T, object>>? orderExpression = null,
            bool isDescending = false,
            int skip = 0,
            int take = 0,
            bool isTracking = true,
            params string[] includes);

        Task<T> GetByIDAsync(int id);
        Task AddAsync(T entity);
        void DeleteAsync(T entity);
        void UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
