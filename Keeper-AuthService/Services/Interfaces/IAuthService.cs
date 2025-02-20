using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ServiceResponse<UsersDTO?>> Registration(CreateUserDTO newUser);
        public Task<ServiceResponse<UsersDTO?>> UserActivation(UserActivationDTO activation);
    }
}
