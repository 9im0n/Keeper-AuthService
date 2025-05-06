using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.DTO;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IDTOMapper
    {
        public PendingActivationDTO Map(PendingActivation pendingActivation);
        public RefreshTokenDTO Map(RefreshToken refreshToken);
    }
}
