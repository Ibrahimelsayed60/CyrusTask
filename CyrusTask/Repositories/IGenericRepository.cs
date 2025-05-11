using CyrusTask.Models;
using System.Linq.Expressions;

namespace CyrusTask.Repositories
{
    public interface IGenericRepository<T> where T: class
    {
        Task<IQueryable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task<T> First(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Delete(T entity);

        bool Exists(int id);

        Task SaveChangesAsync();

    }
}
