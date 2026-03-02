using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GameGarage.Models;

public partial class GameGarageLiteContext : DbContext
{
    public GameGarageLiteContext()
    {
    }

    public GameGarageLiteContext(DbContextOptions<GameGarageLiteContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GamesLite> GamesLites { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=GameGarageLite.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GamesLite>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("GamesLite");

            entity.Property(e => e.AboutTheGame).HasColumnName("About the game");
            entity.Property(e => e.Achievements).HasColumnType("BIGINT");
            entity.Property(e => e.AppId)
                .HasColumnType("BIGINT")
                .HasColumnName("AppID");
            entity.Property(e => e.AveragePlaytimeForever)
                .HasColumnType("BIGINT")
                .HasColumnName("Average playtime forever");
            entity.Property(e => e.AveragePlaytimeTwoWeeks)
                .HasColumnType("BIGINT")
                .HasColumnName("Average playtime two weeks");
            entity.Property(e => e.DiscountDlcCount)
                .HasColumnType("BIGINT")
                .HasColumnName("DiscountDLC count");
            entity.Property(e => e.EstimatedOwners).HasColumnName("Estimated owners");
            entity.Property(e => e.FullAudioLanguages).HasColumnName("Full audio languages");
            entity.Property(e => e.HeaderImage).HasColumnName("Header image");
            entity.Property(e => e.Linux).HasColumnType("BOOL");
            entity.Property(e => e.Mac).HasColumnType("BOOL");
            entity.Property(e => e.MedianPlaytimeForever)
                .HasColumnType("BIGINT")
                .HasColumnName("Median playtime forever");
            entity.Property(e => e.MedianPlaytimeTwoWeeks)
                .HasColumnType("BIGINT")
                .HasColumnName("Median playtime two weeks");
            entity.Property(e => e.MetacriticScore)
                .HasColumnType("BIGINT")
                .HasColumnName("Metacritic score");
            entity.Property(e => e.MetacriticUrl).HasColumnName("Metacritic url");
            entity.Property(e => e.Negative).HasColumnType("BIGINT");
            entity.Property(e => e.PeakCcu)
                .HasColumnType("BIGINT")
                .HasColumnName("Peak CCU");
            entity.Property(e => e.Positive).HasColumnType("BIGINT");
            entity.Property(e => e.Price).HasColumnType("NUMERIC");
            entity.Property(e => e.Recommendations).HasColumnType("BIGINT");
            entity.Property(e => e.ReleaseDate).HasColumnName("Release date");
            entity.Property(e => e.RequiredAge)
                .HasColumnType("BIGINT")
                .HasColumnName("Required age");
            entity.Property(e => e.ScoreRank)
                .HasColumnType("BOOL")
                .HasColumnName("Score rank");
            entity.Property(e => e.SupportEmail).HasColumnName("Support email");
            entity.Property(e => e.SupportUrl).HasColumnName("Support url");
            entity.Property(e => e.SupportedLanguages).HasColumnName("Supported languages");
            entity.Property(e => e.UserScore)
                .HasColumnType("BIGINT")
                .HasColumnName("User score");
            entity.Property(e => e.Windows).HasColumnType("BOOL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
