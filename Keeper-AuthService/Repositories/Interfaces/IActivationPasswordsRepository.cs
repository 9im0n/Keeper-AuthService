using Keeper_AuthService.Models.Db;

namespace Keeper_AuthService.Repositories.Interfaces
{
    public interface IActivationPasswordsRepository : IBaseRepository<ActivationPasswords>
    {
        public Task<ActivationPasswords> GetByEmailAsync(string email);
    }
}
