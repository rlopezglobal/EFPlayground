using System.Text.Json;
using System.Text.Json.Serialization;
using EFPlayground.DbModels;
using EFPlayground.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EFPlayground.Controllers
{

  [Route("api/[controller]")]
  [ApiController]
  public class VideoGameReviewsController : ControllerBase
  {
    private readonly VideoGameDbContext _context;

    private readonly IMapper _mapper;
    /// <summary>
    /// Constructor that injects the DbContext
    /// </summary>
    /// <param name="DbContext to inject"></param>
    public VideoGameReviewsController(VideoGameDbContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }

    /// <summary>
    /// Endpoint to get all video game reviews
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VideoGameReviewDb>>> GetVideoGameReviews()
    {
      var videoGameReviews = await _context.VideoGameReviews
        .Include(vr => vr.VideoGame) // Eager load the VideoGame entity
        .ToListAsync();

      var videoGameReviewDtos = videoGameReviews.Select(vr => new VideoGameReviewDto
      {
        Id = vr.Id,
        ReviewerName = vr.ReviewerName,
        ReviewText = vr.ReviewText,
        Rating = vr.Rating,
        VideoGame = new VideoGameDto
        {
          Id = vr.VideoGame.Id,
          Title = vr.VideoGame.Title,
          Genre = vr.VideoGame.Genre,
          Price = vr.VideoGame.Price,
          ReleaseDate = vr.VideoGame.ReleaseDate
        }
      });
      
     // Use Newtonsoft.Json to format the JSON response
     var settings = new JsonSerializerSettings
     {
       ContractResolver = new CamelCasePropertyNamesContractResolver(),
       Formatting = Formatting.Indented,
       ReferenceLoopHandling = ReferenceLoopHandling.Ignore
     };

     var json = JsonConvert.SerializeObject(videoGameReviews, settings);

      return Content(json, "application/json");
    }

    /// <summary>
    /// Endpoint to get a specific video game review by ID
    /// </summary>
    /// <param name="id">The ID of the video game review to retrieve</param>
    /// <returns>The video game review with the specified ID</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<VideoGameReviewDb>> GetVideoGameReview(string id)
    {
      if (!int.TryParse(id, out int reviewId))
      {
        return BadRequest("Invalid review ID");
      }

      var videoGameReview = await _context.VideoGameReviews
        .Include(vr => vr.VideoGame) // Eager load the VideoGame entity
        .FirstOrDefaultAsync(vr => vr.Id == reviewId);

      if (videoGameReview == null)
      {
        return NotFound();
      }

      var dto = new VideoGameReviewDto
      {
        Id = videoGameReview.Id,
        ReviewerName = videoGameReview.ReviewerName,
        ReviewDate = videoGameReview.ReviewDate,
        ReviewText = videoGameReview.ReviewText,
        Rating = videoGameReview.Rating,
        VideoGame = new VideoGameDto
        {
          Id = videoGameReview.VideoGame.Id,
          Title = videoGameReview.VideoGame.Title,
          Genre = videoGameReview.VideoGame.Genre,
          Price = videoGameReview.VideoGame.Price,
          ReleaseDate = videoGameReview.VideoGame.ReleaseDate
        }
      };

      return Ok(dto);


      /*
      var options = new JsonSerializerOptions
      {
        ReferenceHandler = ReferenceHandler.Preserve,
        MaxDepth = 64, // Increase the maximum depth to 64
        WriteIndented = true // Set WriteIndented property to true
      };

      var json = JsonSerializer.Serialize(videoGameReview, options);

      return Content(json, "application/json");*/
    }

    /// <summary>
    /// Endpoint to create a new videogame review.
    /// </summary>
    /// <param name="createDto">DTO containing information needed to create a new videogame review.</param>
    /// <returns>A 201 created response with the newly created videogame review.</returns>
    [HttpPost]
    public async Task<ActionResult<CreateVideoGameReviewDto>> PostVideoGameReview(CreateVideoGameReviewDto createDto)
    {
        // Map the DTO to the entity using AutoMapper
        var videoGameReview = _mapper.Map<VideoGameReviewDb>(createDto);

        // Add the new video game review to the database
        _context.VideoGameReviews.Add(videoGameReview);
        await _context.SaveChangesAsync();

        // Map the entity back to a DTO using AutoMapper and return it
        var resultDto = _mapper.Map<CreateVideoGameReviewDto>(videoGameReview);
        return CreatedAtAction(nameof(GetVideoGameReview), new { id = videoGameReview.Id }, resultDto);
    }

    /// <summary>
    /// Endpoint to update an existing videogame review
    /// </summary>
    /// <param name="id">The ID of the videogame review to update</param>
    /// <param name="videoGameReview">The updated videogame review data</param>
    /// <returns>A No Content response indicating success</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutVideoGameReview(int id, UpdateVideoGameReviewDto updateDto)
    {
      var videoGameReview = await _context.VideoGameReviews.FindAsync(id);

      if (videoGameReview == null)
      {
        return NotFound();
      }

      // Map the DTO to the entity using AutoMapper
      _mapper.Map(updateDto, videoGameReview);

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
      
      // Map the entity back to a DTO using AutoMapper and return it 
      var resultDto = _mapper.Map<UpdateVideoGameReviewDto>(videoGameReview);
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