using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Web.Backend.Models;

namespace Web.Backend.Interfaces
{
    public interface IFileImageRepository
    {
        Task<FileImage> UploadAsync(IFormFile file);

        Task<FileImage> GetById(int id);
    }
}
