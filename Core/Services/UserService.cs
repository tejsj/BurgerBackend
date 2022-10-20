using Core.Dtos;
using Core.Services.Interfaces;
using Database;
using Microsoft.EntityFrameworkCore;

namespace Core.Services;

public class UserService : IUserService
{
    private readonly BurgerBackendDbContext _context;
    public UserService(BurgerBackendDbContext context)
    {
        _context = context;
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
