using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services
{
    public interface IFileImageRepository
    {
        Task<FileImage> CreateAsync(FileImage fileImage);

        Task<FileImage> GetById(int id);

        Task<FileImage> DeleteAsync(int id);
    }
}
