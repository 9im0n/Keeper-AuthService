using Keeper_AuthService.Models.DB;

namespace Keeper_AuthService.Repositories.Interfaces
{
    public interface IPendingActivationsRepository : IBaseRepository<PendingActivation>
    {
        public Task<PendingActivation?> GetByEmailAsync(string email); 
    }
}
