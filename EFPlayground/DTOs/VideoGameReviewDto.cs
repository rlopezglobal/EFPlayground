namespace EFPlayground.DTOs;

public class VideoGameReviewDto
{
    public int Id { get; set; }
    public string ReviewerName { get; set; }
    public DateTime ReviewDate { get; set; }
    public string ReviewText { get; set; }
    public int Rating { get; set; }
    public VideoGameDto VideoGame { get; set; }
}