using Keeper_AuthService.Models.DB;

namespace Keeper_AuthService.Repositories.Interfaces
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshTokens>
    {
        public Task<ICollection<RefreshTokens>> GetByUserIdAsync(Guid Id);
        public Task<RefreshTokens?> GetByTokenAsync(string token);
        public Task<RefreshTokens?> GetValidTokenByToken(string token);
        public Task<RefreshTokens?> GetValidTokenByUserId(Guid Id);
    }
}
