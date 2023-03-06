using EFPlayground.DbModels;

namespace EFPlayground;

public class DbInitializer
{
    /// <summary>
    /// This method ensures that the database is created if it doesn't exist yet and adds sample data 
    /// </summary>
    /// <param name="context"></param>
    
    public static void Initialize(VideoGameDbContext context)
    {
        
        
        // This is to ensure a clean database on every start of the application
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        // Add sample data to the VideoGames database if none exists
        if (!context.VideoGames.Any())
        {
            context.VideoGames.Add(new VideoGameDb {Title = "Super Mario Bros", Genre = "Platformer", Price = (decimal) 59.99, ReleaseDate = 1985});
            context.VideoGames.Add(new VideoGameDb {Title = "The Legend of Zelda", Genre = "Action-Adventure", Price = (decimal) 59.99, ReleaseDate = 1986});
            context.VideoGames.Add(new VideoGameDb {Title = "Final Fantasy VII", Genre = "RPG", Price = (decimal) 59.99, ReleaseDate = 1997 });
            context.VideoGames.Add(new VideoGameDb {Title = "ChopLifter", Genre = "Action", Price = (decimal) 59.99, ReleaseDate = 1986 }); 
            context.VideoGames.Add(new VideoGameDb {Title = "Metroid", Genre = "Action", Price = (decimal) 59.99, ReleaseDate = 1991 });
            context.VideoGames.Add(new VideoGameDb {Title = "DoubleDragon", Genre = "Action", Price = (decimal) 59.99, ReleaseDate = 1991 });
            context.SaveChangesAsync();
        }
        
        // Add sample data to the VideoGameReviews database if none exists
        if (!context.VideoGameReviews.Any())
        {
            var videoGame = context.VideoGames.FirstOrDefault(v => v.Title == "Super Mario Bros");

            context.VideoGameReviews.Add(new VideoGameReviewDb { VideoGameId = videoGame.Id, ReviewerName = "John Smith", ReviewDate = DateTime.Now, ReviewText = "This game is super cool", Rating = 5});

            context.SaveChangesAsync();
        }
    }
}