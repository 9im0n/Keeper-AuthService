using Keeper_AuthService.Models.DB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Keeper_AuthService.DB
{
    public class AppDbContext : DbContext
    {
        // Tables
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<PendingActivation> PendingActivations { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
