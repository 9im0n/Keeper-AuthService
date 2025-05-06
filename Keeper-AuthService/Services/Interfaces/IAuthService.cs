using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ServiceResponse<object?>> Register(RegisterDTO newUser);
        public Task<ServiceResponse<SessionDTO?>> Login(LoginDTO login);
        public Task<ServiceResponse<object?>> Logout(Guid userId);
        public Task<ServiceResponse<object?>> Activation(ActivationDTO activation);
        public Task<ServiceResponse<string?>> UpdateJwt(UpdateJwtDTO updateJwt);
    }
}
