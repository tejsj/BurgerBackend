using Core.Dtos;
using Core.Services.Interfaces;
using Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Core.Services;

public class AuthenticateService : IAuthenticateService
{
    private readonly IConfiguration _configuration;
    private readonly BurgerBackendDbContext _context;
    public AuthenticateService(IConfiguration configuration, BurgerBackendDbContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public async Task<string> Authenticate(LoginDto user)
    {
        var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserName == user.UserName && x.Password == user.Password);
        if (dbUser == null)
            throw new Exception("User does not exists!");

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                    new(ClaimTypes.Name, dbUser.Name),
                    new("UserId", dbUser.Id.ToString())
            }),

            Expires = DateTime.UtcNow.AddMinutes(10),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature),
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
        };

        if (!string.IsNullOrEmpty(dbUser.Role))
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, dbUser.Role));

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);

    }

    public async Task<List<UserDto>> GetAllUsers()
    {
        return await _context.Users.Select(x => new UserDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToListAsync();
    }
}
