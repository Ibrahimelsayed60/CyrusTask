
using CyrusTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CyrusTask.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly ProjectManagementContext _pMContext;

        public GenericRepository(ProjectManagementContext PMContext)
        {
            _pMContext = PMContext;
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            return _pMContext.Set<T>().Where(x => !x.IsDeleted).AsNoTrackingWithIdentityResolution();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var data = await GetAllAsync();
            return await data.AsTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _pMContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _pMContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            Update(entity);
        }

        public bool Exists(int id)
        {
            return _pMContext.Set<T>().Where(x => !x.IsDeleted).Any(x => x.Id == id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _pMContext.SaveChangesAsync();
        }

        public async Task<T> First(Expression<Func<T, bool>> predicate)
        {
            return await (await GetAllAsync()).Where(predicate).FirstOrDefaultAsync();
        }
    }
}
