using Keeper_AuthService.DB;
using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_AuthService.Repositories.Implementations
{
    public class PendingActivationsRepository : BaseRepository<PendingActivation>, IPendingActivationsRepository
    {
        public PendingActivationsRepository(AppDbContext context) : base(context) { }

        public async Task<PendingActivation?> GetByEmailAsync(string email)
        {
            return await _appDbContext.PendingActivations.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
