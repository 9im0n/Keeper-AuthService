using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        public Task<ServiceResponse<string>> CreateAsync(Guid userId);
        public Task<ServiceResponse<RefreshTokens?>> RevokeTokenAsync(Guid userId);
        public Task<ServiceResponse<RefreshTokens?>> ValidateTokenAsync(string token);
        public Task<ServiceResponse<RefreshTokens?>> RotateTokenAsync(string token);
    }
}
