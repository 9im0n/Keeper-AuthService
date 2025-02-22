using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using Keeper_AuthService.Models.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Keeper_AuthService.Services.Implementations
{
    public class JwtService : IJwtService
    {
        private readonly JwtSettings _jwtSettings;

        public JwtService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<ServiceResponse<string?>> GenerateTokenAsync(UsersDTO user)
        {
            List<Claim> claims = new List<Claim>() { new Claim("UserID", user.Id.ToString()) };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenExpirationMinutes),
                signingCredentials: new SigningCredentials(_jwtSettings.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                );

            string jwtToken =  new JwtSecurityTokenHandler().WriteToken(jwt);
            return ServiceResponse<string?>.Success(jwtToken);
        }
    }
}
