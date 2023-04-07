using EFPlayground.DbModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EFPlayground
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(VideoGameDbContext context)
        {
            // This is to ensure a clean database on every start of the application
            await context.Database.EnsureDeletedAsync();
            await context.Database.EnsureCreatedAsync();

            // Add sample data to the VideoGames database if none exists 
            if (!context.VideoGames.Any())
            {
                context.VideoGames.Add(new VideoGameDb { Title = "Super Mario Bros", Genre = "Platformer", Price = 59.99m, ReleaseDate = 1985 });
                context.VideoGames.Add(new VideoGameDb { Title = "The Legend of Zelda", Genre = "Action-Adventure", Price = 59.99m, ReleaseDate = 1986 });
                context.VideoGames.Add(new VideoGameDb { Title = "Final Fantasy VII", Genre = "RPG", Price = 59.99m, ReleaseDate = 1997 });
                context.VideoGames.Add(new VideoGameDb { Title = "ChopLifter", Genre = "Action", Price = 59.99m, ReleaseDate = 1986 });
                context.VideoGames.Add(new VideoGameDb { Title = "Metroid", Genre = "Action", Price = 59.99m, ReleaseDate = 1991 });
                context.VideoGames.Add(new VideoGameDb { Title = "DoubleDragon", Genre = "Action", Price = 59.99m, ReleaseDate = 1991 });
                await context.SaveChangesAsync();
            }

            // Add sample data to the VideoGameReviews database if none exists
            if (!context.VideoGameReviews.Any())
            {
                var videoGame = context.VideoGames.FirstOrDefault(v => v.Title == "Super Mario Bros");

                context.VideoGameReviews.Add(new VideoGameReviewDb { VideoGameId = videoGame.Id, ReviewerName = "John Smith", ReviewDate = DateTime.Now, ReviewText = "This game is super cool", Rating = 5 });
                
                var videoGame2 = context.VideoGames.FirstOrDefault(v => v.Title == "The Legend of Zelda");

                context.VideoGameReviews.Add(new VideoGameReviewDb { VideoGameId = videoGame2.Id, ReviewerName = "Robbert", ReviewDate = DateTime.Now, ReviewText = "This game is Awesome-O", Rating = 5 });

                await context.SaveChangesAsync();
            }
        }
    }
}