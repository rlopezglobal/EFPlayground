namespace EFPlayground.DTOs;

public class UpdateVideoGameReviewDto
{
    public int Id { get; set; } 
    public int Rating { get; set; }
    public string ReviewerName { get; set; }
    public string ReviewText { get; set; }
}