namespace Core.Dtos;

public class RestaurantDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public double DistanceInMeters { get; set; }

}
