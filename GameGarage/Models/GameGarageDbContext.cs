using Microsoft.EntityFrameworkCore;

namespace GameGarage.Models;

public partial class GameGarageDbContext : DbContext
{
    public GameGarageDbContext(DbContextOptions<GameGarageDbContext> options) 
        : base(options) { }

    public DbSet<Game> Games { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GamesLite");

            entity.Property(e => e.Id)
                .HasColumnType("BIGINT")
                .HasColumnName("AppID");

            entity.Property(e => e.Name).HasColumnName("Name");

            entity.Property(e => e.ReleaseDate).HasColumnName("Release date");

            entity.Property(e => e.Price).HasColumnType("NUMERIC");

            entity.Property(e => e.AboutTheGame).HasColumnName("About the game");

            entity.Property(e => e.Notes).HasColumnName("Notes");

            entity.Property(e => e.Developers).HasColumnName("Developers");

            entity.Property(e => e.Publishers).HasColumnName("Publishers");

            entity.Property(e => e.Categories).HasColumnName("Categories");

            entity.Property(e => e.HeaderImage).HasColumnName("Header image");

            entity.Property(e => e.Screenshots)
            .HasConversion(e => 
            string.Join(',', e ?? Enumerable.Empty<string>()), 
            e => e.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .HasColumnName("Screenshots");

            entity.Property(e => e.Tags)
            .HasConversion(e => 
            string.Join(',', e ?? Enumerable.Empty<string>()), 
            e => e.Split(',', StringSplitOptions.RemoveEmptyEntries))
            .HasColumnName("Tags");

            entity.Property(e => e.Linux)
                .HasColumnType("BOOL")
                .HasColumnName("Linux");
            entity.Property(e => e.Mac)
                .HasColumnType("BOOL")
                .HasColumnName("Mac");
            entity.Property(e => e.Windows)
                .HasColumnType("BOOL")
                .HasColumnName("Windows");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
