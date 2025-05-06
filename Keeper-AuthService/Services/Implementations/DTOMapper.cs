using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implementations
{
    public class DTOMapper : IDTOMapper
    {
        public PendingActivationDTO Map(PendingActivation pendingActivation)
        {
            return new PendingActivationDTO()
            {
                Id = pendingActivation.Id,
                Email = pendingActivation.Email,
                PasswordHash = pendingActivation.PasswordHash,
                ActivationCodeHash = pendingActivation.ActivationCodeHash,
                CreatedAt = pendingActivation.CreatedAt,
                ExpiresAt = pendingActivation.ExpiresAt
            };
        }

        public RefreshTokenDTO Map(RefreshToken refreshToken)
        {
            return new RefreshTokenDTO()
            {
                Id = refreshToken.Id,
                UserId = refreshToken.UserId,
                Token = refreshToken.Token,
                CreatedAt = refreshToken.CreatedAt,
                ExpiresAt = refreshToken.ExpiresAt,
                Revoked = refreshToken.Revoked
            };
        }
    }
}
