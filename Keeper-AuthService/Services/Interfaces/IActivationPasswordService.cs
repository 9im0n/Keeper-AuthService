using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IActivationPasswordService
    {
        public ServiceResponse<ActivationPasswordDTO> Generate();
        public ServiceResponse<object?> SendByEmail(string email, ActivationPasswordDTO activationPasswordDTO);
    }
}
