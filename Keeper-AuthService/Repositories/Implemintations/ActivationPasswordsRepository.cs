using Keeper_AuthService.DB;
using Keeper_AuthService.Models.Db;
using Keeper_AuthService.Repositories.Interfaces;
using Keeper_UserService.Repositories.Implemintations;
using Microsoft.EntityFrameworkCore;

namespace Keeper_AuthService.Repositories.Implemintations
{
    public class ActivationPasswordsRepository : BaseRepository<ActivationPasswords>, IActivationPasswordsRepository
    {
        public ActivationPasswordsRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task<ActivationPasswords?> GetByEmailAsync(string email)
        {
            return await _appDbContext.ActivationPasswords.FirstOrDefaultAsync(x => x.Email == email);
        }
    }
}
