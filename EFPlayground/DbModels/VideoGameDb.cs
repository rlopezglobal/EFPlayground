using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFPlayground.DbModels;

public class VideoGameDb
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [ReadOnly(true)]
    public int Id { get; set; }
    
    public string? Title { get; set; }
    public string Genre { get; set; } = "";
    public decimal Price { get; set; }
    public int ReleaseDate { get; set; }
}