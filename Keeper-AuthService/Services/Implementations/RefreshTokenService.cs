using Keeper_AuthService.Models.DB;
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

        public RefreshTokenService(IRefreshTokensRepository refreshTokensRepository, IOptions<JwtSettings> jwtSettings)
        {
            _refreshTokensRepository = refreshTokensRepository;
            _jwtSettings = jwtSettings.Value;
        }


        public async Task<ServiceResponse<string>> CreateAsync(Guid userId)
        {
            string token = GenerateToken();

            RefreshTokens refreshToken = new RefreshTokens()
            {
                UserId = userId,
                Token = HashToken(token),
                ExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            };

            await _refreshTokensRepository.CreateAsync(refreshToken);

            return ServiceResponse<string>.Success(token);
        }


        public async Task<ServiceResponse<RefreshTokens?>> RevokeTokenAsync(Guid userId)
        {
            RefreshTokens? refreshToken = await _refreshTokensRepository.GetByUserIdAsync(userId);

            if (refreshToken == null)
                return ServiceResponse<RefreshTokens?>.Fail(default, 404, "This user doesn't have refresh token.");

            refreshToken.Revoked = true;
            await _refreshTokensRepository.UpdateAsync(refreshToken);

            return ServiceResponse<RefreshTokens?>.Success(refreshToken);
        }


        public async Task<ServiceResponse<RefreshTokens?>> ValidateTokenAsync(string token)
        {
            RefreshTokens? refreshTokens = await _refreshTokensRepository.GetValidToken(HashToken(token));

            if (refreshTokens == null)
                return ServiceResponse<RefreshTokens?>.Fail(default, 404, "Refresh Token doesn't exist.");

            return ServiceResponse<RefreshTokens?>.Success(refreshTokens);
        }


        public async Task<ServiceResponse<RefreshTokens?>> RotateTokenAsync(string token)
        {
            RefreshTokens? refreshTokens = await _refreshTokensRepository.GetByTokenAsync(HashToken(token));

            if (refreshTokens == null)
                return ServiceResponse<RefreshTokens?>.Fail(default, 404, "Refresh Token doesn't exist.");

            refreshTokens.ExpiresAt.AddDays(7);
            refreshTokens = await _refreshTokensRepository.UpdateAsync(refreshTokens);

            return ServiceResponse<RefreshTokens?>.Success(refreshTokens);
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


        public string HashToken(string token)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(token));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
