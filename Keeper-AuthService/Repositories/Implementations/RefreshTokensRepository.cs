using Keeper_AuthService.DB;
using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_AuthService.Repositories.Implementations
{
    public class RefreshTokensRepository : BaseRepository<RefreshTokens>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(AppDbContext context) : base(context) { }


        public async Task<RefreshTokens?> GetByUserIdAsync(Guid Id)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.UserId == Id);
        }


        public async Task<RefreshTokens?> GetByTokenAsync(string token)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);
        }


        public async Task<RefreshTokens?> GetValidToken(string token)
        {
            return await _appDbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token &&
                t.ExpiresAt > DateTime.UtcNow && !t.Revoked);
        }
    }
}
