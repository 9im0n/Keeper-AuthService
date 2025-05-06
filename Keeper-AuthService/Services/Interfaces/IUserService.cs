using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<UserDTO?>> GetByIdAsync(Guid Id);
        public Task<ServiceResponse<UserDTO?>> CreateAsync(CreateUserDTO createUserDTO);
        public Task<ServiceResponse<UserDTO?>> ActivateUser(ActivationDTO activation);
        public Task<ServiceResponse<UserDTO?>> GetByEmailAsync(string email);
        public Task<ServiceResponse<FullUserDTO?>> GetFullUserByEmailAsync(string email);
    }
}
