using Keeper_AuthService.DB;
using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_AuthService.Repositories.Implementations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected readonly AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }


        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(obj => obj.Id == id);
        }


        public async Task<T> CreateAsync(T obj)
        {
            await _appDbContext.Set<T>().AddAsync(obj);
            await _appDbContext.SaveChangesAsync();
            return obj;
        }


        public async Task<T?> UpdateAsync(T obj)
        {
            T? oldObj = await _appDbContext.Set<T>().FirstOrDefaultAsync(obj => obj.Id == obj.Id);

            if (oldObj == null)
                return null;

            _appDbContext.Entry(oldObj).CurrentValues.SetValues(obj);
            await _appDbContext.SaveChangesAsync();

            return oldObj;
        }


        public async Task<T?> DeleteAsync(Guid id)
        {
            T? obj = await _appDbContext.Set<T>().FirstOrDefaultAsync(obj => obj.Id == id);

            if (obj == null)
                return null;

            _appDbContext.Remove(obj);

            await _appDbContext.SaveChangesAsync();
            return obj;
        }
    }
}
