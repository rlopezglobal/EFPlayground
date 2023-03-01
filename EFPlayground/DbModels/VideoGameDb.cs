namespace EFPlayground.DbModels;

public class VideoGameDb
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string Genre { get; set; } = "";
    public decimal Price { get; set; }
    public int ReleaseDate { get; set; }
}