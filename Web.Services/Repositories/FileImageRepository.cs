using System.Threading.Tasks;
using Web.ShareModels;

namespace Web.Services
{
    public class FileImageRepository : IFileImageRepository
    {
        private readonly ApplicationDbContext _context;

        public FileImageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FileImage> DeleteAsync(int id)
        {
            var fileImage = await _context.FileImages.FindAsync(id);
            if (fileImage == null)
                return null;
            _context.Remove(fileImage);
            await _context.SaveChangesAsync();
            return fileImage;
        }

        public async Task<FileImage> GetById(int id)
        {
            var fileImage = await _context.FileImages.FindAsync(id);
            return fileImage;
        }

        public async Task<FileImage> CreateAsync(FileImage fileImage)
        {
           _context.FileImages.Add(fileImage);
            await _context.SaveChangesAsync();
            return fileImage; 
        }
    }
}
