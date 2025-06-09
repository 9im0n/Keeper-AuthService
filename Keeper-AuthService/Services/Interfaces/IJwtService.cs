using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IJwtService
    {
        public ServiceResponse<JwtDTO?> GenerateToken(UserDTO user);
    }
}
