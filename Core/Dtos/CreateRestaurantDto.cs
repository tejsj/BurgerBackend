using System.ComponentModel.DataAnnotations;

namespace Core.Dtos;

public class CreateRestaurantDto
{
    [Required]
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    [Required]
    public string Street { get; set; } = null!;
    [Required]
    public string City { get; set; } = null!;
    [Required]
    public int ZipCode { get; set; }
    [Required]
    public string OpeningHours { get; set; } = null!;
    [Required]
    public double Latitude { get; set; }
    [Required]
    public double Longitude { get; set; }
}
