using Keeper_AuthService.Models.DTO;
using Keeper_AuthService.Models.Services;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IEmailService
    {
        public Task<ServiceResponse<object?>> SendWelcomeEmailAsync(string email, ActivationPasswordDTO password);
    }
}
