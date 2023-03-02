
using EFPlayground.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFPlayground.Controllers;

[Route("api/[controller]")]
public class VideoGamesController : ControllerBase
{
    private readonly VideoGameDbContext _context;

    public VideoGamesController(VideoGameDbContext context)
    {
        _context = context;
    }
    
    // GET: api/VideoGames
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameDb>>> GetVideoGames()
    {
        return await _context.VideoGames.ToListAsync();
    }
    
    // Get: api/VideoGames/5
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
    
    // POST: api/VideoGames
    [HttpPost]
    public async Task<ActionResult<VideoGameDb>> PostVideoGame(VideoGameDb videoGame)
    {
        _context.VideoGames.Add(videoGame);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetVideoGame), new {id = videoGame.Id}, videoGame);
    }
    
    // PUT: api/VideoGames/5
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
    
    // DELETE: api/VideoGames/5
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

    
    private bool VideoGameExists(int id)
    {
        return _context.VideoGames.Any(e => e.Id == id);
    }
   

}