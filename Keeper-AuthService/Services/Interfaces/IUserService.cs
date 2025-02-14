using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<UsersDTO?>> CreateAsync(CreateUserDTO newUser);
        //public Task<ServiceResponse<UsersDTO?>> ActivateUser(string email);
    }
}
