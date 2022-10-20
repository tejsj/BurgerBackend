using Core.Dtos;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        public RestaurantsController(
            IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<RestaurantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllRestaurants(double? latitude, double? longitude)
        {
            try
            {
                return Ok(await _restaurantService.GetAllRestaurants(latitude, longitude));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }            
        }       

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RestaurantDetailsDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRestaurantById(Guid id, double? latitude, double? longitude)
        {
            try
            {
                return Ok(await _restaurantService.GetRestaurantById(id, latitude, longitude));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }            
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantDto dto)
        {
            try
            {
                await _restaurantService.CreateRestaurant(dto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [Authorize]
        [HttpPost("Rate")]
        public async Task<IActionResult> RateRestaurant([FromBody] CreateRatingDto dto)
        {
            try
            {
                await _restaurantService.RateRestaurant(dto);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet("Nearby")]
        [ProducesResponseType(typeof(List<RestaurantDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetRestaurantsNearby(double distanceMeters, double? latitude, double? longitude)
        {
            try
            {
                return Ok(await _restaurantService.GetNearbyRestaurants(distanceMeters, latitude, longitude));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
