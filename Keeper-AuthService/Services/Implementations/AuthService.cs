using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;
using Keeper_AuthService.Models.DB;
using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;


namespace Keeper_AuthService.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly IPendingActivationService _pendingActivationService;
        private readonly IActivationPasswordService _activationPasswordService;
        private readonly IEmailService _emailService;
        private readonly IDTOMapper _mapper;

        public AuthService(IUserService userService, 
            IJwtService jwtService, 
            IRefreshTokenService refreshTokenService,
            IPendingActivationService pendingActivationService,
            IActivationPasswordService activationPasswordService,
            IEmailService emailService,
            IDTOMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _refreshTokenService = refreshTokenService;
            _pendingActivationService = pendingActivationService;
            _activationPasswordService = activationPasswordService;
            _emailService = emailService;
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

            ActivationPasswordDTO activationPasswordDTO = _activationPasswordService.Generate().Data!;

            ServiceResponse<object?> emailServiceResponse = await _emailService.SendWelcomeEmailAsync(registerDTO.Email, activationPasswordDTO);

            if (!emailServiceResponse.IsSuccess)
                return ServiceResponse<object?>.Fail(default, emailServiceResponse.Status, emailServiceResponse.Message);

            CreatePendingActivationDTO createPendingActivationDTO = new CreatePendingActivationDTO
            {
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                ActivationCodeHash = BCrypt.Net.BCrypt.HashPassword(activationPasswordDTO.Password)
            };

            ServiceResponse<PendingActivationDTO?> createPendingResponse = await _pendingActivationService
                .CreateAsync(createPendingActivationDTO);

            if (!createPendingResponse.IsSuccess)
                return ServiceResponse<object?>.Fail(default, createPendingResponse.Status, createPendingResponse.Message);

            return ServiceResponse<object?>.Success(default, 201);
        }


        public async Task<ServiceResponse<SessionDTO?>> Login(LoginDTO login)
        {
            ServiceResponse<PendingActivationDTO?> pendingActivationServiceResponse = await _pendingActivationService
                .GetByEmailAsync(login.Email);

            if (pendingActivationServiceResponse.IsSuccess)
                return ServiceResponse<SessionDTO?>.Fail(default, 422, "User was registered, but didn't activate.");

            ServiceResponse<FullUserDTO?> userServiceResponse = await _userService
                .GetFullUserByEmailAsync(login.Email);

            if (!userServiceResponse.IsSuccess)
                return ServiceResponse<SessionDTO?>
                    .Fail(default, userServiceResponse.Status, userServiceResponse.Message);

            if (!BCrypt.Net.BCrypt.Verify(login.Password, userServiceResponse.Data!.Password))
                return ServiceResponse<SessionDTO?>.Fail(default, 400, "Passwords don't match.");

            UserDTO userDTO = new UserDTO()
            {
                Id = userServiceResponse.Data.Id,
                Email = userServiceResponse.Data.Email,
                Role = userServiceResponse.Data.Role,
                Profile = userServiceResponse.Data.Profile,
            };

            ServiceResponse<JwtDTO?> jwtResponse = _jwtService.GenerateToken(userDTO);

            ServiceResponse<string> refreshResponse = await _refreshTokenService
                .CreateAsync(userServiceResponse.Data.Id);

            userDTO = new UserDTO()
            {
                Id = userServiceResponse.Data.Id,
                Email = userServiceResponse.Data.Email,
                Role = userServiceResponse.Data.Role,
                Profile = userServiceResponse.Data.Profile
            };

            TokensDTO tokenDTO = new TokensDTO()
            {
                AccessToken = jwtResponse.Data!.AccessToken,
                RefreshToken = refreshResponse.Data!
            };

            SessionDTO sessionDTO = new SessionDTO
            {
                User = userDTO,
                Tokens = tokenDTO
            };

            return ServiceResponse<SessionDTO>.Success(sessionDTO);
        }


        public async Task<ServiceResponse<object?>> Logout(Guid userId)
        {
            return await _refreshTokenService.RevokeTokensAsync(userId);
        }


        public async Task<ServiceResponse<object?>> Activation(ActivationDTO activation)
        {
            ServiceResponse<PendingActivationDTO?> pendingActivationResponse = await _pendingActivationService
                .GetByEmailAsync(activation.Email);

            if (!pendingActivationResponse.IsSuccess)
                return ServiceResponse<object?>.Fail(default, 404, "User haven't registered.");

            if (!BCrypt.Net.BCrypt.Verify(activation.ActivationPassword,
                pendingActivationResponse.Data?.ActivationCodeHash))
                return ServiceResponse<object?>.Fail(default, 400, "Passwords don't mathc.");

            CreateUserDTO createUserDTO = new CreateUserDTO
            {
                Email = activation.Email,
                Password = pendingActivationResponse.Data?.PasswordHash!
            };

            ServiceResponse<UserDTO?> userResponse = await _userService.CreateAsync(createUserDTO);

            if (!userResponse.IsSuccess)
                return ServiceResponse<object?>.Fail(default, userResponse.Status, userResponse.Message);

            pendingActivationResponse = await _pendingActivationService
                .DeleteAsync(pendingActivationResponse.Data!.Id);

            if (!pendingActivationResponse.IsSuccess)
                return ServiceResponse<object?>.Fail(default, pendingActivationResponse.Status, pendingActivationResponse.Message);

            return ServiceResponse<object?>.Success(default);
        }


        public async Task<ServiceResponse<JwtDTO?>> UpdateJwt(UpdateJwtDTO updateJwt)
        {
            ServiceResponse<RefreshTokenDTO?> refreshResponse = await _refreshTokenService
                .ValidateTokenAsync(updateJwt.RefreshToken);

            if (!refreshResponse.IsSuccess)
                return ServiceResponse<JwtDTO?>.Fail(default, refreshResponse.Status, refreshResponse.Message);

            ServiceResponse<UserDTO?> userResponse = await _userService.GetByIdAsync(refreshResponse.Data!.UserId);

            if (!userResponse.IsSuccess)
                return ServiceResponse<JwtDTO?>.Fail(default, userResponse.Status, userResponse.Message);

            return _jwtService.GenerateToken(userResponse.Data!);
        }
    }
}
