using Database.Models;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public class RatingDto
{
    [Required]
    [Range(1, 5)]
    public int TasteRating { get; set; }
    [Required]
    [Range(1, 5)]
    public int TextureRating { get; set; }
    [Required]
    [Range(1, 5)]
    public int VisualPresentationRating { get; set; }
    public string? ImagePath { get; set; }
    public DateTime RatedAt { get; set; }
    public UserDto RatedByUser { get; set; } = null!;
}
