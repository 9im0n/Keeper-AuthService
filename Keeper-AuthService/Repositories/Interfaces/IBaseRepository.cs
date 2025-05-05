using Keeper_AuthService.Models.DB;

namespace Keeper_AuthService.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseModel
    {
        public Task<List<T>> GetAllAsync();
        public Task<T?> GetByIdAsync(Guid id);
        public Task<T> CreateAsync(T obj);
        public Task<T?> UpdateAsync(T obj);
        public Task<T?> DeleteAsync(Guid id);
    }
}
