using EfBasics.Models;
using Microsoft.EntityFrameworkCore;

namespace EfBasics.Data
{
    public class LarpPlayersDbContext : DbContext
    {
        public DbSet<LarpPlayer> LarpPlayers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Weapon> Weapons { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseLazyLoadingProxies()    
            .UseSqlServer(
                @"Server=localhost;Database=Larpers;Trusted_Connection=True");

    }
}
