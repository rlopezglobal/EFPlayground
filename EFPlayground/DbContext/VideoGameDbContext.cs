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

            base.OnModelCreating(modelBuilder);
        }
    }


}