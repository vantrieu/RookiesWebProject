using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Data;
using Web.Backend.Interfaces;
using Web.Backend.Models;

namespace Web.Backend.Repositories
{
    public class FileImageRepository : IFileImageRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileImageRepository(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<FileImage> GetById(int id)
        {
            var fileImage = await _context.FileImages.FindAsync(id);
            return fileImage;
        }

        public Task<FileImage> UploadAsync(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                FileImage temp = new FileImage();
                temp.FileLocation = $"/images/{uniqueFileName}";
                temp.CreateDate = DateTime.Today;
                return CreateAsync(temp);
            }
            else
            {
                return null;
            }
        }

        private async Task<FileImage> CreateAsync(FileImage model)
        {
            _context.FileImages.Add(model);
            await _context.SaveChangesAsync();
            return model;
        }
    }
}
