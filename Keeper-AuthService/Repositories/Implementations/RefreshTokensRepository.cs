using Keeper_AuthService.DB;
using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_AuthService.Repositories.Implementations
{
    public class RefreshTokensRepository : BaseRepository<RefreshTokens>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(AppDbContext context) : base(context) { }


        public async Task<List<RefreshTokens>> GetByUserIdAsync(Guid Id)
        {
            return await _appDbContext.RefreshTokens.Where(t => t.UserId == Id).ToListAsync();
        }


        public async Task<RefreshTokens?> GetByTokenAsync(string token)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }


        public async Task<RefreshTokens?> GetValidTokenByToken(string token)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token &&
                t.ExpiresAt > DateTime.UtcNow && !t.Revoked);
        }


        public async Task<RefreshTokens?> GetValidTokenByUserId(Guid Id)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == Id && 
                t.ExpiresAt > DateTime.UtcNow && !t.Revoked);
        }
    }
}
