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
                Email = pendingActivation.Email,
                PasswordHash = pendingActivation.PasswordHash,
                ActivationCodeHash = pendingActivation.ActivationCodeHash,
                CreatedAt = pendingActivation.CreatedAt,
                ExpiresAt = pendingActivation.ExpiresAt
            };
        }
    }
}
