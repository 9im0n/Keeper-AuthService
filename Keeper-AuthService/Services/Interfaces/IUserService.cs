using Keeper_ApiGateWay.Models.Services;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<bool>> CreateAsync(CreateUserDTO newUser);
        public Task<ServiceResponse<UsersDTO?>> ActivateUser(string email);
    }
}
