namespace EFPlayground.DTOs;

public class CreateVideoGameReviewDto
{
    public int VideoGameId { get; set; }
    public string ReviewerName { get; set; }
    public string ReviewText { get; set; }
    public int Rating { get; set; }
}