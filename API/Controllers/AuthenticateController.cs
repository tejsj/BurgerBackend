using Core.Dtos;
using Core.Services.Interfaces;
using Database.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly IAuthenticateService _authenticateService;
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        [AllowAnonymous]
        [HttpPost, Route("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> Login(LoginDto loginDTO)
        {
            try
            {
                var token = await _authenticateService.Authenticate(loginDTO);
                if (!string.IsNullOrEmpty(token))
                    return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Unauthorized();
        }
    }
}
