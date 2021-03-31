using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Backend.Interfaces
{
    public interface IFileImageRepository
    {
        Task<FileImage> UploadAsync(IFormFile file);

        Task<FileImage> GetById(int id);

        Task<FileImage> DeleteAsync(int id);
    }
}
