using Keeper_AuthService.Models.Db;
using Microsoft.EntityFrameworkCore;


namespace Keeper_AuthService.DB
{
    public class AppDbContext : DbContext
    {
        // Tables
        public DbSet<ActivationPasswords> ActivationPasswords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}
