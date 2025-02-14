using Keeper_AuthService.Models.Services;
using Keeper_AuthService.Models.Db;

namespace Keeper_AuthService.Services.Interfaces
{
    public interface IActivationPasswordsService
    {
        //public Task<ServiceResponse<bool>> ChaeckPasswordAsync(string email, string password);
        public Task<ServiceResponse<ActivationPasswords>> CreateAsync(string email);
    }
}
