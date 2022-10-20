using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public class CreateRatingDto
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
    public IFormFile? ImageFile { get; set; }
    [Required]
    public Guid RestaurantId { get; set; }
}
