using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ServiceResponse<UsersDTO?>> Registration(CreateUserDTO newUser);
        public Task<ServiceResponse<UserInfoDTO?>> Login(LoginDTO login);
        public Task<ServiceResponse<UsersDTO?>> Logout(LogoutDTO logout);
        public Task<ServiceResponse<UsersDTO?>> UserActivation(UserActivationDTO activation);
        public Task<ServiceResponse<string?>> UpdateJwt(UpdateJwtDTO updateJwt);
    }
}
