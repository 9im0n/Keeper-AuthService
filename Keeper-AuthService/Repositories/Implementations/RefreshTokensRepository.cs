using Keeper_AuthService.DB;
using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Keeper_AuthService.Repositories.Implementations
{
    public class RefreshTokensRepository : BaseRepository<RefreshToken>, IRefreshTokensRepository
    {
        public RefreshTokensRepository(AppDbContext context) : base(context) { }


        public async Task<List<RefreshToken>> GetByUserIdAsync(Guid Id)
        {
            return await _appDbContext.RefreshToken.Where(t => t.UserId == Id).ToListAsync();
        }


        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _appDbContext.RefreshToken.FirstOrDefaultAsync(t => t.Token == token);
        }


        public async Task<RefreshToken?> GetValidTokenByToken(string token)
        {
            return await _appDbContext.RefreshToken.FirstOrDefaultAsync(t => t.Token == token &&
                t.ExpiresAt > DateTime.UtcNow && !t.Revoked);
        }


        public async Task<RefreshToken?> GetValidTokenByUserId(Guid Id)
        {
            return await _appDbContext.RefreshToken.FirstOrDefaultAsync(t => t.UserId == Id && 
                t.ExpiresAt > DateTime.UtcNow && !t.Revoked);
        }

        public async Task RevokeValidTokensAsync(Guid userId)
        {
            var sql = @"UPDATE ""RefreshTokens""
                SET ""Revoked"" = TRUE
                WHERE ""UserId"" = @userId
                    AND ""ExpiresAt"" > NOW()
                    AND NOT ""Revoked""";

            await _appDbContext.Database.ExecuteSqlRawAsync(sql, new Npgsql.NpgsqlParameter("@userId", userId));
        }
    }
}
