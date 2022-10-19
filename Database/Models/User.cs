namespace Database.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Role { get; set; }
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public ICollection<Rating> MyRatings { get; set; } = new List<Rating>();
}
