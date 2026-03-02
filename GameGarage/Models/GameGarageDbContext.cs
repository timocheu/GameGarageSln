using Microsoft.EntityFrameworkCore;

namespace GameGarage.Models;

public class GameGarageDbContext : DbContext
{
    public GameGarageDbContext(DbContextOptions<GameGarageDbContext> options) 
        : base(options) { }

    public DbSet<Game> Games { get; set; }
}
