using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Services.Interfaces;

namespace Keeper_AuthService.Services.Implementations
{
    public class ActivationPasswordService : IActivationPasswordService
    {
        public ServiceResponse<ActivationPasswordDTO> Generate()
        {
            throw new NotImplementedException();
        }

        public ServiceResponse<object?> SendByEmail(string email,
            ActivationPasswordDTO activationPasswordDTO)
        {
            throw new NotImplementedException();
        }
    }
}
