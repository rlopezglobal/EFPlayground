using EFPlayground.DbModels;

namespace EFPlayground;

public class DbInitializer
{
    public static void Initialize(VideoGameDbContext context)
    {
        // create database if it doesn't exist yet 
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        
        // Add sample data 
        if (!context.VideoGames.Any())
        {
            context.VideoGames.Add(new VideoGameDb {Title = "Super Mario Bros", Genre = "Platformer", Price = (decimal) 59.99, ReleaseDate = 1985});
            context.VideoGames.Add(new VideoGameDb {Title = "The Legend of Zelda", Genre = "Action-Adventure", Price = (decimal) 59.99, ReleaseDate = 1986});
            context.VideoGames.Add(new VideoGameDb {Title = "Final Fantasy VII", Genre = "RPG", Price = (decimal) 59.99, ReleaseDate = 1997 });
            context.VideoGames.Add(new VideoGameDb {Title = "ChopLifter", Genre = "Action", Price = (decimal) 59.99, ReleaseDate = 1986 }); 
            context.VideoGames.Add(new VideoGameDb {Title = "Metroid", Genre = "Action", Price = (decimal) 59.99, ReleaseDate = 1991 });
            context.SaveChangesAsync();
        }
    }
}