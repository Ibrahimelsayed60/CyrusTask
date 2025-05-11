
using CyrusTask.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task SaveChangesAsync()
        {
            await _pMContext.SaveChangesAsync();
        }

        
    }
}
