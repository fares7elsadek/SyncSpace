using Microsoft.AspNetCore.Http;

namespace SyncSpace.Application.Services;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file, string[] allowedExtensions);
    void DeleteFile(string FileName);
}
