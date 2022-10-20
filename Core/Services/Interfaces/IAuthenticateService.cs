using Core.Dtos;

namespace Core.Services.Interfaces;

public interface IAuthenticateService
{
    Task<string> Authenticate(LoginDto user);
}
