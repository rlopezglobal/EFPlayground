using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFPlayground.DbModels
{
    /// <summary>
    /// Represents a video game in the database.
    /// </summary>
    public class VideoGameDb
    {
        /// <summary>
        /// The primary key for the video game in the database.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ReadOnly(true)]
        public int Id { get; set; }

        /// <summary>
        /// The title of the video game.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The genre of the video game.
        /// </summary>
        public string Genre { get; set; }

        /// <summary>
        /// The price of the video game.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The release date of the video game.
        /// </summary>
        public int ReleaseDate { get; set; }

        /// <summary>
        /// A concurrency token used to detect changes to the video game entity in the database.
        /// </summary>
        [Timestamp]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// A collection of reviews for this video game.
        /// </summary>
        public ICollection<VideoGameReviewDb> VideoGameReviews { get; set; }
    }
}