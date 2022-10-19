namespace Database.Models;

public class Rating
{
    public Guid Id { get; set; }
    public int TasteRating { get; set; }
    public int TextureRating { get; set; }
    public int VisualPresentationRating { get; set; }
    public DateTime RatedAt { get; set; }
    public string? ImagePath { get; set; }
    public Guid RatedByUserId { get; set; }
    public User RatedByUser { get; set; } = null!;
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;
}
