using Core.Implementation.Database.EntityFramework;
using EFPlayground.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace EFPlayground
{
    public class VideoGameDbContext : DbContext
    {
        public VideoGameDbContext(DbContextOptions<VideoGameDbContext> options) : base(options)
        {
        }

        public DbSet<VideoGameDb> VideoGames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VideoGameDb>().ToTable("VideoGames", schema: "dbo");
            modelBuilder.Entity<VideoGameDb>().HasKey(v => v.Id);

            // Add this line to drop and recreate the table
            modelBuilder.Entity<VideoGameDb>().ToTable("VideoGames", schema: "dbo");

            base.OnModelCreating(modelBuilder);
        }
    }


}