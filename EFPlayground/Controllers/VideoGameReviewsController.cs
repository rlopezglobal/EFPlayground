using EFPlayground.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFPlayground.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class VideoGameReviewsController : ControllerBase
  {
    private readonly VideoGameDbContext _context;

    /// <summary>
    /// Constructor that injects the DbContext
    /// </summary>
    /// <param name="DbContext to inject"></param>
    public VideoGameReviewsController(VideoGameDbContext context)
    {
      _context = context;
    }

    /// <summary>
    /// Endpoint to get all video game reviews
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameReviewDb>>> GetVideoGameReviews()
    {
      return await _context.VideoGameReviews.ToListAsync();
    }

    /// <summary>
    /// Endpoint to get a specific video game review by ID
    /// </summary>
    /// <param name="id">The ID of the video game review to retrieve</param>
    /// <returns>The video game review with the specified ID</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGameReviewDb>> GetVideoGameReview(int id)
    {
      var videoGameRview = await _context.VideoGameReviews.FindAsync(id);

      if (videoGameRview == null)
      {
        return NotFound();
      }

      return videoGameRview;
    }


    /// <summary>
    /// Endpoint to create a new videogame review
    /// </summary>
    /// <param name="videoGameReview">Videogame review to create</param>
    /// <returns>A 201 created response with the newly created video game review</returns>
    [HttpPost]
    public async Task<ActionResult<VideoGameReviewDb>> PostVideoGameReview(VideoGameReviewDb videoGameReview)
    {
      _context.VideoGameReviews.Add(videoGameReview);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetVideoGameReviews), new {id = videoGameReview.VideoGameId}, videoGameReview);
    }

    /// <summary>
    /// Endpoint to update an existing videogame review
    /// </summary>
    /// <param name="id">The ID of the videogame review to update</param>
    /// <param name="videoGameReview">The updated videogame review data</param>
    /// <returns>A No Content response indicating success</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<VideoGameReviewDb>> PutVideoGameReview(int id, VideoGameReviewDb videoGameReview)
    {
      if (id != videoGameReview.VideoGameId)
      {
        return BadRequest();
      }

      _context.Entry(videoGameReview).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!VideoGameReviewExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();

    }

    /// <summary>
    /// Endpoint to delete a videogame review by ID
    /// </summary>
    /// <param name="id">The ID of the videogame revierw to delete</param>
    /// <returns>A No content response indicating success</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVideoGameReview(int id)
    {
      
      var videoGameReview = await _context.VideoGameReviews.FindAsync(id);
      
      if (videoGameReview == null)
      {
        return NotFound();
      }
      
      _context.VideoGameReviews.Remove(videoGameReview);

      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool VideoGameReviewExists(int id)
    {
      return _context.VideoGameReviews.Any(e => e.VideoGameId == id);
    }

  }
}