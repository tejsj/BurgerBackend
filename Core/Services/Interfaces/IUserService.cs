using Core.Dtos;

namespace Core.Services.Interfaces;

public interface IUserService
{  
    Task<List<UserDto>> GetAllUsers();
}
