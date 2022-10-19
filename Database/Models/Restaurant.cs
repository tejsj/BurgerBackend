using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database.Models;

public class Restaurant
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public string Street { get; set; } = null!;
    public string City { get; set; } = null!;
    public int ZipCode { get; set; }
    public string OpeningHours { get; set; } = null!;
   
    [Column(TypeName = "geography")]
    public Point Location { get; set; } = null!;
    public ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
