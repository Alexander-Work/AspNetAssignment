using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GameConnect.Models;

namespace GameConnect.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<GameConnect.Models.Game> Game { get; set; } = default!;
        public DbSet<GameConnect.Models.GamePlayer> GamePlayer { get; set; } = default!;
    }
}