using Keeper_AuthService.Models.DB;
using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IPendingActivationService
    {
        public Task<ServiceResponse<PendingActivationDTO?>> GetByEmailAsync(string email);
        public Task<ServiceResponse<PendingActivationDTO?>> CreateAsync(CreatePendingActivationDTO createDTO);
        public ServiceResponse<string> Generate();
    }
}
