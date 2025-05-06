using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IRefreshTokenService
    {
        public Task<ServiceResponse<string>> CreateAsync(Guid userId);
        public Task<ServiceResponse<object?>> RevokeTokensAsync(Guid userId);
        public Task<ServiceResponse<RefreshTokenDTO?>> ValidateTokenAsync(string token);
        public Task<ServiceResponse<RefreshTokenDTO?>> RotateTokenAsync(string token);
    }
}
