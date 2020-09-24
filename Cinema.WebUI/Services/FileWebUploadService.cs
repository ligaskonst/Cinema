using System.IO;
using System.Threading.Tasks;
using Cinema.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Cinema.WebUI.Services
{
    public class FileWebUploadService : IFileUploadService
    {
        private readonly IWebHostEnvironment _environment;

        public FileWebUploadService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task UploadFile(string path, byte[] file)
        {
            await using MemoryStream memoryStream = new MemoryStream(file);
            await using FileStream fileStream = new FileStream(GetFilePath(path), FileMode.Create);
            
            await memoryStream.CopyToAsync(fileStream);
        }

        public void RemoveFile(string path)
        {
            var filePath = GetFilePath(path);

            if (File.Exists(filePath))
                File.Delete(filePath);
        }

        private string GetFilePath(string path) => _environment.WebRootPath + path.Replace('/','\\');
    }
}
