using Keeper_AuthService.Models.DB;

namespace Keeper_AuthService.Repositories.Interfaces
{
    public interface IRefreshTokensRepository : IBaseRepository<RefreshToken>
    {
        public Task<List<RefreshToken>> GetByUserIdAsync(Guid Id);
        public Task<RefreshToken?> GetByTokenAsync(string token);
        public Task<RefreshToken?> GetValidTokenByToken(string token);
        public Task<RefreshToken?> GetValidTokenByUserId(Guid Id);
        public Task RevokeValidTokensAsync(Guid userId);
    }
}
