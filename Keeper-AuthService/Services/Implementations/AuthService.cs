using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;
using Keeper_AuthService.Models.DB;
using BCrypt.Net;


namespace Keeper_AuthService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IPendingActivationService _pendingActivationService;
        private readonly IDTOMapper _mapper;

        public AuthService(IUserService userService, 
            IJwtService jwtService, 
            IRefreshTokenService refreshTokenService,
            IPendingActivationService pendingActivationService,
            IDTOMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _pendingActivationService = pendingActivationService;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<object?>> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.Password != registerDTO.ConfirmPassword)
                return ServiceResponse<object?>.Fail(default, 400, "Passwords don't match.");

            ServiceResponse<PendingActivationDTO?> pendingActivationServiceResponse = await _pendingActivationService
                .GetByEmailAsync(registerDTO.Email);

            if (pendingActivationServiceResponse.IsSuccess)
                return ServiceResponse<object>.Fail(default, 422, "User was registered, but didn't activate.");

            ServiceResponse<UserDTO?> userServiceResponse = await _userService.GetByEmailAsync(registerDTO.Email);

            if (userServiceResponse.IsSuccess)
                return ServiceResponse<object?>.Fail(default, 409, "User had registered.");


        }


        public async Task<ServiceResponse<SessionDTO?>> Login(LoginDTO login)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResponse<object?>> Logout(LogoutDTO logout)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResponse<object?>> Activation(ActivationDTO activation)
        {
            throw new NotImplementedException();
        }


        public async Task<ServiceResponse<string?>> UpdateJwt(UpdateJwtDTO updateJwt)
        {
            throw new NotImplementedException();
        }
    }
}
