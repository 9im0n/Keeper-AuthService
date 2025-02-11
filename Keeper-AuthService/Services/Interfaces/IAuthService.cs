using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.Db;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<ServiceResponse<ActivationPasswords?>> Registration(CreateUserDTO newUser);
    }
}
