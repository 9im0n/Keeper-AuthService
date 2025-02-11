using Keeper_AuthService.DB;
using Keeper_AuthService.Models.Db;
using Keeper_AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_UserService.Repositories.Implemintations
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected AppDbContext _appDbContext;

        public BaseRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _appDbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {
            return await _appDbContext.Set<T>().FirstOrDefaultAsync(obj => obj.Id == id);
        }


        public async Task<T> CreateAsync(T obj)
        {
            await _appDbContext.Set<T>().AddAsync(obj);
            return obj;
        }


        public async Task<T> UpdateAsync(T obj)
        {
            T oldObj = await _appDbContext.Set<T>().FirstOrDefaultAsync(obj => obj.Id == obj.Id);

            if (oldObj == null)
                return null;

            _appDbContext.Entry(oldObj).CurrentValues.SetValues(obj);
            await _appDbContext.SaveChangesAsync();

            return oldObj;
        }


        public async Task<T> DeleteAsync(int id)
        {
            T obj = await _appDbContext.Set<T>().FirstOrDefaultAsync(obj => obj.Id == id);
            _appDbContext.Remove(obj);

            await _appDbContext.SaveChangesAsync();
            return obj;
        }
    }
}
