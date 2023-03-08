using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.JavaScript;

namespace EFPlayground.DbModels;

/// <summary>
/// Represents a video game review entity in the database.
/// </summary>
[Table("VideoGameReviews")]
public class VideoGameReviewDb
{
    /// <summary>
    /// Gets or sets the primary key for this entity.
    /// </summary>
    [Key]
    public int Id { get; set; }
    
    /// <summary>
    /// Gets or sets the foreign key that references the associated video game entity.
    /// </summary>
    [ForeignKey("VideoGame")]
    public int VideoGameId { get; set; }
    
    /// <summary>
    /// Gets or sets the navigation property that represents the associated video game entity.
    /// </summary>
    [Required]
    public VideoGameDb VideoGame { get; set; }

    /// <summary>
    /// Gets or sets the name of the reviewer for this review.
    /// </summary>
    [Required]
    [StringLength(50)]
    public string ReviewerName { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this review was created.
    /// </summary>
    [Required]
    public DateTime ReviewDate { get; set; }

    /// <summary>
    /// Gets or sets the text of the review.
    /// </summary>
    [Required]
    [StringLength(500)]
    public string ReviewText { get; set; }

    /// <summary>
    /// Gets or sets the rating of the review (between 1 and 5).
    /// </summary>
    [Required]
    [Range(1, 5)]
    public int Rating { get; set; }
}
