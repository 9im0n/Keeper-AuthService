using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.Settings;
using Keeper_AuthService.Repositories.Interfaces;
using Keeper_AuthService.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;


namespace Keeper_AuthService.Services.Implementations
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokensRepository _refreshTokensRepository;
        private readonly JwtSettings _jwtSettings;
        private readonly IDTOMapper _mapper;

        public RefreshTokenService(IRefreshTokensRepository refreshTokensRepository, 
            IOptions<JwtSettings> jwtSettings,
            IDTOMapper mapper)
        {
            _refreshTokensRepository = refreshTokensRepository;
            _jwtSettings = jwtSettings.Value;
            _mapper = mapper;
        }


        public async Task<ServiceResponse<string>> CreateAsync(Guid userId)
        {
            await RevokeTokensAsync(userId);

            string token = GenerateToken();

            RefreshToken refreshToken = new RefreshToken()
            {
                UserId = userId,
                Token = HashToken(token),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            await _refreshTokensRepository.CreateAsync(refreshToken);

            return ServiceResponse<string>.Success(token);
        }


        public async Task<ServiceResponse<object?>> RevokeTokensAsync(Guid userId)
        {
            await _refreshTokensRepository.RevokeValidTokensAsync(userId);
            return ServiceResponse<object?>.Success(default);
        }


        public async Task<ServiceResponse<RefreshTokenDTO?>> ValidateTokenAsync(string token)
        {
            RefreshToken? refreshToken = await _refreshTokensRepository.GetValidTokenByToken(HashToken(token));

            if (refreshToken == null)
                return ServiceResponse<RefreshTokenDTO?>.Fail(default, 404, "Refresh Token doesn't exist.");

            RefreshTokenDTO refreshTokenDTO = _mapper.Map(refreshToken);

            return ServiceResponse<RefreshTokenDTO?>.Success(refreshTokenDTO);
        }


        public async Task<ServiceResponse<RefreshTokenDTO?>> RotateTokenAsync(string token)
        {
            RefreshToken? refreshToken = await _refreshTokensRepository.GetByTokenAsync(HashToken(token));

            if (refreshToken == null)
                return ServiceResponse<RefreshTokenDTO?>.Fail(default, 404, "Refresh Token doesn't exist.");

            refreshToken.ExpiresAt.AddDays(7);
            refreshToken = await _refreshTokensRepository.UpdateAsync(refreshToken);

            RefreshTokenDTO refreshTokenDTO = _mapper.Map(refreshToken);

            return ServiceResponse<RefreshTokenDTO?>.Success(refreshTokenDTO);
        }


        private string GenerateToken()
        {
            byte[] numbers = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(numbers);
            }
            return Convert.ToBase64String(numbers);
        }


        private string HashToken(string token)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
