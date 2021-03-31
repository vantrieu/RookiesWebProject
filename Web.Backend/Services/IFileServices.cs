using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Web.Backend.Services
{
    public interface IFileServices
    {
        bool UploadFileAsync(string filePath, IFormFile file);

        bool DeleteFileAsync(string fileName);
    }
}
