
using EFPlayground.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFPlayground.Controllers;

/// <summary>
/// Controller for endpoints related to video games
/// </summary>
[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly VideoGameDbContext _context;
    
    /// <summary>
    /// Constructor that injects the VideoGameDbContext
    /// </summary>
    // <param name="context">The VideoGameDbContext to use</param>
    public VideoGamesController(VideoGameDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// GET endpoint to retrieve all video games
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameDb>>> GetVideoGames()
    {
        return await _context.VideoGames.ToListAsync();
    }
    
    /// <summary>
    /// GET endpoint to retrieve a specific video game by ID
    /// </summary>
    /// <param name="id">The ID of the video game to retrieve</param>
    /// <returns>The video game with the specified ID</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGameDb>> GetVideoGame(int id)
    {
        
        var videoGame = await _context.VideoGames.FindAsync(id);
        
        if (videoGame == null)
        {
            return NotFound();
        }
        
        return videoGame;
    }
    
    /// <summary>
    /// POST endpoint to create a new video game
    /// </summary>
    /// <param name="videoGame">The new video game to create</param>
    /// <returns>The newly created video game</returns>
    [HttpPost]
    public async Task<ActionResult<VideoGameDb>> PostVideoGame(VideoGameDb videoGame)
    {
        _context.VideoGames.Add(videoGame);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetVideoGame), new {id = videoGame.Id}, videoGame);
    }
    
    // <summary>
    /// PUT endpoint to update an existing video game, return badrequest if not found
    /// </summary>
    /// <param name="id">The ID of the video game to update</param>
    /// <param name="videoGame">The updated video game object</param>
    /// <returns>An IActionResult indicating the success of the update operation</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateVideoGame(int id, [FromBody] VideoGameDb videoGame)
    {
        
        if (id != videoGame.Id)
        {
            return BadRequest();
        }
        
        var existingVideoGame = await _context.VideoGames.FindAsync(id);
        
        if (existingVideoGame == null)
        {
            return NotFound();
        }
        
        existingVideoGame.Title = videoGame.Title;
        existingVideoGame.Genre = videoGame.Genre;
        existingVideoGame.Price = videoGame.Price;
        existingVideoGame.ReleaseDate = videoGame.ReleaseDate;
        
        _context.VideoGames.Update(existingVideoGame);
        await _context.SaveChangesAsync();
        return NoContent();
    }
    
    /// <summary>
    /// DELETE endpoint to delete a video game by ID
    /// </summary>
    /// <param name="id">The ID of the video game to delete</param>
    /// <returns>An IActionResult indicating the success of the delete operation</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideoGame(int id)
    {
        var videoGame = await _context.VideoGames.FindAsync(id);
        
        if (videoGame == null)
        {
            return NotFound();
        }
        
        _context.VideoGames.Remove(videoGame);
        
        await _context.SaveChangesAsync();
        
        return NoContent();
    }

}