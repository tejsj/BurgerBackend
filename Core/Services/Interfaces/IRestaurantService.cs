using Core.Dtos;
using Database.Models;

namespace Core.Services.Interfaces;

public interface IRestaurantService
{
    Task<List<RestaurantDto>> GetNearbyRestaurants(double maxDistanceMeters, double? latitude, double? longitude);
    Task<List<RestaurantDto>> GetAllRestaurants(double? latitude, double? longitude);
    Task<RestaurantDetailsDto> GetRestaurantById(Guid id, double? latitude, double? longitude);
    Task RateRestaurant(CreateRatingDto rating);
    Task CreateRestaurant(CreateRestaurantDto restaurant);
}
