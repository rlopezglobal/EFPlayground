using Core.Implementation.Database.EntityFramework;
using EFPlayground.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFPlayground
{
    // Define the class inheriting from the DbContext class
    public class VideoGameDbContext : DbContext
    {
        // Define a DbSet property for VideoGameDb and VideoGameReviews objects
        public DbSet<VideoGameDb> VideoGames { get; set; }
        public DbSet<VideoGameReviewDb> VideoGameReviews { get; set; } // <-- Add semicolon here
        
        // Constructor for the VideoGameDbContext that takes in a DbContextOptions object
        public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options)
        {
        }

        // Override OnModelCreating method to configure entity model properties
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<VideoGameDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Title).IsRequired();
                entity.Property(e => e.Genre).IsRequired();
                entity.Property(e => e.Price).HasColumnType("decimal(18,2)").IsRequired();
                entity.Property(e => e.ReleaseDate).IsRequired();
            });

            modelBuilder.Entity<VideoGameReviewDb>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ReviewerName).IsRequired();
                entity.Property(e => e.ReviewDate).IsRequired();
                entity.Property(e => e.ReviewText).IsRequired();
                entity.Property(e => e.Rating).IsRequired();
                entity.HasOne(e => e.VideoGame)
                    .WithMany(v => v.VideoGameReviews)
                    .HasForeignKey(e => e.VideoGameId);
            });
        }
    }
}