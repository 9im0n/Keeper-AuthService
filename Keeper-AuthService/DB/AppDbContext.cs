using Keeper_AuthService.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Keeper_AuthService.DB
{
    public class AppDbContext : DbContext
    {
        // Tables
        public DbSet<RefreshTokens> RefreshTokens;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
