using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;


namespace Keeper_AuthService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthService(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public async Task<ServiceResponse<UsersDTO?>> Registration(CreateUserDTO newUser)
        {
            ServiceResponse<UsersDTO?> createUserResponse = await _userService.CreateAsync(newUser);

            if (!createUserResponse.IsSuccess)
                return ServiceResponse<UsersDTO?>.Fail(null, createUserResponse.Status, createUserResponse.Message);

            return ServiceResponse<UsersDTO?>.Success(createUserResponse.Data, 201, createUserResponse.Message);
        }


        public async Task<ServiceResponse<string?>> Login(LoginDTO login)
        {
            ServiceResponse<UsersDTO?> userResponse = await _userService.GetByEmailAsync(login.Email);

            if (!userResponse.IsSuccess)
                return ServiceResponse<string?>.Fail(default, userResponse.Status, userResponse.Message);

            if (!userResponse.Data.IsActive)
                return ServiceResponse<string?>.Fail(default, 400, "User isn't activ");

            if (userResponse.Data?.Password != login.Password)
                return ServiceResponse<string?>.Fail(default, 400, "Passwords doesn't match.");

            return await _jwtService.GenerateToken(userResponse.Data);
        }


        public async Task<ServiceResponse<UsersDTO?>> UserActivation(UserActivationDTO activation)
        {
            return await _userService.ActivateUser(activation);
        }
    }
}
