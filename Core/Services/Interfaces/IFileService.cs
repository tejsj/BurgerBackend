using Microsoft.AspNetCore.Http;

namespace Core.Services.Interfaces;

public interface IFileService
{
    Task<string?> Upload(IFormFile? file);
}
