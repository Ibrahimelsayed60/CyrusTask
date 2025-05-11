using CyrusTask.Models;
using CyrusTask.Specifications;
using System.Linq.Expressions;

namespace CyrusTask.Repositories
{
    public interface IGenericRepository<T> where T: BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync();

        Task<T?> GetByIdAsync(int id);

        Task<T?> GetWithSpecAsync(ISpecifications<T> spec);

        Task<int> GetCountAsync(ISpecifications<T> spec);

        Task<T> AddAsync(T entity);

        Task<T> First(Expression<Func<T, bool>> predicate);

        Task<IQueryable<T>> GetAllWithExpresion(Expression<Func<T, bool>> predicate);

        void Update(T entity);

        void Delete(T entity);

        bool Exists(int id);

        Task<int> SaveChangesAsync();

    }
}
