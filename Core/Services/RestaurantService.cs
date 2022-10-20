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
        private readonly IFileService _fileService;
        public RestaurantService(
            BurgerBackendDbContext context, 
            IHttpContextAccessor httpContextAccessor,
            IFileService fileService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _fileService = fileService;
        }
        public async Task<List<RestaurantDto>> GetNearbyRestaurants(double distanceMeters, double? latitude, double? longitude)
        {
            var location = new Point(12.58254, 55.68024) { SRID = 4326};
            if (latitude.HasValue && longitude.HasValue)
                location = new Point(longitude.Value, latitude.Value) { SRID = 4326 };

            var restaurants = await _context.Restaurants
                .OrderBy(x => x.Location.Distance(location))
                .Where(x => x.Location.IsWithinDistance(location, distanceMeters))
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
                        ImagePath = y.ImagePath,
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

        public async Task RateRestaurant(CreateRatingDto ratingDto)
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.Claims?.FirstOrDefault(x => x.Type.Equals("UserId", StringComparison.OrdinalIgnoreCase))?.Value;
            if (string.IsNullOrEmpty(currentUserId))
                throw new Exception("User Id not set.");

            var uid = Guid.Parse(currentUserId);
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == uid);
            if (user == null)
                throw new Exception("User not found in datastore");
            var restaurant = await _context.Restaurants.FirstOrDefaultAsync(x => x.Id == ratingDto.RestaurantId);
            if (restaurant == null)
                throw new Exception("Restaurant not found in datastore");

            var newRating = new Rating
            {
                Id = Guid.NewGuid(),
                TasteRating = ratingDto.TasteRating,
                TextureRating = ratingDto.TextureRating,
                VisualPresentationRating = ratingDto.VisualPresentationRating,
                RatedAt = DateTime.Now,
                RatedByUser = user,
                Restaurant = restaurant,
                ImagePath = await _fileService.Upload(ratingDto.ImageFile)
            };

            _context.Ratings.Add(newRating);

            await _context.SaveChangesAsync();
        }

        public async Task CreateRestaurant(CreateRestaurantDto restaurantDto)
        {
            var newRestaurant = new Restaurant
            {
                Id = Guid.NewGuid(),
                Name = restaurantDto.Name,
                Description = restaurantDto.Description,
                Street = restaurantDto.Street,
                City = restaurantDto.City,
                ZipCode = restaurantDto.ZipCode,
                OpeningHours = restaurantDto.OpeningHours,
                Location = new Point(restaurantDto.Longitude, restaurantDto.Latitude) { SRID = 4326 }
            };

            _context.Restaurants.Add(newRestaurant);

            await _context.SaveChangesAsync();
        }
    }
}
