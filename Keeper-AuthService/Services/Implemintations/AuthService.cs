using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implemintations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<ServiceResponse<UsersDTO?>> Registration(CreateUserDTO newUser)
        {
            ServiceResponse<UsersDTO?> createUserResponse = await _userService.CreateAsync(newUser);

            if (!createUserResponse.IsSuccess)
                return ServiceResponse<UsersDTO?>.Fail(null, createUserResponse.Status, createUserResponse.Message);

            return ServiceResponse<UsersDTO?>.Success(createUserResponse.Data, 201, createUserResponse.Message);
        }
    }
}
