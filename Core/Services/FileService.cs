using Core.Services.Interfaces;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Core.Services;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;
    public FileService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string?> Upload(IFormFile? file)
    {
        var basePath = _configuration["UploadBasePath"];
        string uploads = Path.Combine(basePath, "uploads");
        if (!string.IsNullOrEmpty(basePath) && file != null && file.Length > 0)
        {
            Directory.CreateDirectory(uploads);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploads, fileName);
            using (Stream fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return filePath;
        }
        return null;
    }
}
