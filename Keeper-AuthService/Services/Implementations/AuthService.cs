using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;
using Keeper_AuthService.Models.DB;


namespace Keeper_AuthService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthService(IUserService userService, IJwtService jwtService, IRefreshTokenService refreshTokenService)
        {
            _userService = userService;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<ServiceResponse<UsersDTO?>> Registration(CreateUserDTO newUser)
        {
            ServiceResponse<UsersDTO?> createUserResponse = await _userService.CreateAsync(newUser);

            if (!createUserResponse.IsSuccess)
                return ServiceResponse<UsersDTO?>.Fail(null, createUserResponse.Status, createUserResponse.Message);

            return ServiceResponse<UsersDTO?>.Success(createUserResponse.Data, 201, createUserResponse.Message);
        }


        public async Task<ServiceResponse<TokensDTO?>> Login(LoginDTO login)
        {
            ServiceResponse<UsersDTO?> userResponse = await _userService.GetByEmailAsync(login.Email);

            if (!userResponse.IsSuccess)
                return ServiceResponse<TokensDTO?>.Fail(default, userResponse.Status, userResponse.Message);

            if (!userResponse.Data.IsActive)
                return ServiceResponse<TokensDTO?>.Fail(default, 400, "User isn't activ");

            if (userResponse.Data?.Password != login.Password)
                return ServiceResponse<TokensDTO?>.Fail(default, 400, "Passwords doesn't match.");

            ServiceResponse<string?> jwtToken = await _jwtService.GenerateTokenAsync(userResponse.Data);
            ServiceResponse<string> refreshToken = await _refreshTokenService.CreateAsync(userResponse.Data.Id);

            TokensDTO response = new TokensDTO()
            {
                AccessToken = jwtToken.Data,
                RefreshToken = refreshToken.Data
            };

            return ServiceResponse<TokensDTO?>.Success(response);
        }


        public async Task<ServiceResponse<UsersDTO?>> UserActivation(UserActivationDTO activation)
        {
            return await _userService.ActivateUser(activation);
        }


        public async Task<ServiceResponse<string?>> UpdateJwt(UpdateJwtDTO updateJwt)
        {
            ServiceResponse<RefreshTokens?> rtResponse = await _refreshTokenService.ValidateTokenAsync(updateJwt.RefreshToken);

            if (!rtResponse.IsSuccess)
                return ServiceResponse<string?>.Fail(default, rtResponse.Status, rtResponse.Message);

            ServiceResponse<UsersDTO?> user = await _userService.GetByEmailAsync(updateJwt.Email);

            if (!user.IsSuccess)
                return ServiceResponse<string?>.Fail(default, user.Status, user.Message);

            return await _jwtService.GenerateTokenAsync(user.Data);
        }
    }
}
