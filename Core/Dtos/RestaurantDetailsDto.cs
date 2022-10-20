using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos;

public class RestaurantDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public int ZipCode { get; set; }
    public string OpeningHours { get; set; } = null!;
    public double DistanceMeters { get; set; }
    public IEnumerable<RatingDto> Ratings { get; set; } = new List<RatingDto>();
}
