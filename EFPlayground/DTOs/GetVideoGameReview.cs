namespace EFPlayground.DTOs;

public class GetVideoGameReview
{
    public class VideoGameReviewDto 
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public DateTime ReviewDate { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public VideoGameDto VideoGame { get; set; }
    }

    public class VideoGameDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Genre { get; set; }
        public decimal Price { get; set; }
        public int ReleaseDate { get; set; }
    }
}