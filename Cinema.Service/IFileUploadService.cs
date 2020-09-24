using System.Threading.Tasks;

namespace Cinema.Service
{
    public interface IFileUploadService
    {
        Task UploadFile(string filePath, byte[] file);

        void RemoveFile(string filePath);
    }
}
