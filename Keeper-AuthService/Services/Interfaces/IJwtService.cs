using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IJwtService
    {
        public Task<ServiceResponse<string?>> GenerateTokenAsync(UsersDTO user);
    }
}
