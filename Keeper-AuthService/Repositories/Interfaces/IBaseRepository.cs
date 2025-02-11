using Keeper_UserService.Models.Db;

namespace Keeper_AuthService.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<ICollection<T>> GetAllAsync();
        public Task<T> GetByIdAsync(int id);
        public Task<T> CreateAsync(T obj);
        public Task<T> UpdateAsync(T obj);
        public Task<T> DeleteAsync(int id);
    }
}
