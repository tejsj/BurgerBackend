using Core.Services.Interfaces;
using Database.Models;
using NetTopologySuite.Geometries;
using Database;
using Microsoft.EntityFrameworkCore;
using Core.Dtos;
using Microsoft.AspNetCore.Http;

namespace Core.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly BurgerBackendDbContext _context;
        public RestaurantService(BurgerBackendDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<RestaurantDto>> GetNearbyRestaurants(double meters, double? latitude, double? longitude)
        {
            var location = new Point(12.58254, 55.68024) { SRID = 4326};
            if (latitude.HasValue && longitude.HasValue)
                location = new Point(longitude.Value, latitude.Value) { SRID = 4326 };

            var restaurants = await _context.Restaurants
                .OrderBy(x => x.Location.Distance(location))
                .Where(x => x.Location.IsWithinDistance(location, meters))
                .Select(x => new RestaurantDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    DistanceMeters = x.Location.Distance(location)
                })
                .ToListAsync();

            return restaurants;
        }

        public async Task<List<RestaurantDto>> GetAllRestaurants(double? latitude, double? longitude)
        {
            var location = new Point(12.58254, 55.68024) { SRID = 4326 };
            if (latitude.HasValue && longitude.HasValue)
                location = new Point(longitude.Value, latitude.Value) { SRID = 4326 };

            var restaurants = await _context.Restaurants
                .Select(x => new RestaurantDto { 
                    Id = x.Id,
                    Name = x.Name,
                    DistanceMeters = x.Location.Distance(location)
                })
                .ToListAsync();

            return restaurants;
        }

        public async Task<RestaurantDetailsDto> GetRestaurantById(Guid id, double? latitude, double? longitude)
        {
            var location = new Point(12.58254, 55.68024) { SRID = 4326 };
            if (latitude.HasValue && longitude.HasValue)
                location = new Point(longitude.Value, latitude.Value) { SRID = 4326 };

            var restaurant = await _context
                .Restaurants
                .Include(x => x.Ratings)
                .ThenInclude(x => x.RatedByUser)
                .Select(x => new RestaurantDetailsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Street = x.Street,
                    City = x.City,
                    ZipCode = x.ZipCode,
                    OpeningHours = x.OpeningHours,
                    DistanceMeters = x.Location.Distance(location),
                    Ratings = x.Ratings.Select(y => new RatingDto
                    {
                        TasteRating = y.TasteRating,
                        TextureRating = y.TextureRating,
                        VisualPresentationRating = y.VisualPresentationRating,
                        RatedAt = y.RatedAt,
                        RatedByUser = new UserDto()
                        {
                            Id = y.RatedByUser.Id,
                            Name = y.RatedByUser.Name
                        }
                    })
                })
                .FirstOrDefaultAsync(x => x.Id == id);

            if (restaurant == null)
                throw new Exception("Restaurant not found in datastore.");

            return restaurant;
            
        }

        public async Task RateRestaurant(CreateRatingDto rating)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value;
            if (string.IsNullOrEmpty(currentUserId))
                throw new Exception("User Id not set.");

            var uid = Guid.Parse(currentUserId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == uid);
            if (user == null)
                throw new Exception("User not found in datastore");
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == rating.RestaurantId);
            if (restaurant == null)
                throw new Exception("Restaurant not found in datastore");

            var newRating = new Rating
            {
                Id = Guid.NewGuid(),
                TasteRating = rating.TasteRating,
                TextureRating = rating.TextureRating,
                VisualPresentationRating = rating.VisualPresentationRating,
                RatedAt = DateTime.Now,
                RatedByUser = user,
                Restaurant = restaurant,
                ImagePath = rating.ImagePath
            };

            _context.Ratings.Add(newRating);

            await _context.SaveChangesAsync();
        }

        public async Task CreateRestaurant(CreateRestaurantDto restaurant)
        {
            var newRestaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = restaurant.Name,
                Description = restaurant.Description,
                Street = restaurant.Street,
                City = restaurant.City,
                ZipCode = restaurant.ZipCode,
                OpeningHours = restaurant.OpeningHours,
                Location = new Point(restaurant.Longitude, restaurant.Latitude) { SRID = 4326 }
            };

            _context.Restaurants.Add(newRestaurant);

            await _context.SaveChangesAsync();
        }
    }
}
