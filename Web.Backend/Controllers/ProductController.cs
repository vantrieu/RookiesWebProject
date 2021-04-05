using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Web.Backend.Services;
using Web.Services;
using Web.Services.Interfaces;
using Web.ShareModels;
using Web.ShareModels.ViewModels;

namespace Web.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileImageRepository _fileImageRepository;
        private readonly IProductFileImageRepository _productFileImageRepository;
        private readonly IFileServices _fileServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRateRepository _rateRepository;

        public ProductController(IProductRepository productRepository, IFileImageRepository fileImageRepository,
            IProductFileImageRepository productFileImageRepository, IFileServices fileServices,
            IWebHostEnvironment webHostEnvironment, IRateRepository rateRepository)
        {
            _productRepository = productRepository;
            _fileImageRepository = fileImageRepository;
            _productFileImageRepository = productFileImageRepository;
            _fileServices = fileServices;
            _webHostEnvironment = webHostEnvironment;
            _rateRepository = rateRepository;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Create([FromForm] ProductRequestVm model)
        {
            Product product = new Product();
            product.Name = model.Name;
            product.Description = model.Description;
            product.Quantities = model.Quantities;
            product.Price = model.Price;
            product.CreatedDate = DateTime.Today;
            product.UpdatedDate = DateTime.Today;
            product.CategoryId = model.CategoryId;
            var result = await _productRepository.CreateAsync(product);
            if ((model.images != null) && (model.images.Count > 0))
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                foreach (IFormFile file in model.images)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    var kq = _fileServices.UploadFileAsync(filePath, file);
                    if (kq)
                    {
                        FileImage fileImage = new FileImage();
                        fileImage.FileLocation = $"/images/{fileName}";
                        fileImage.CreateDate = DateTime.Today;
                        var temp = await _fileImageRepository.CreateAsync(fileImage);

                        ProductFileImage productFileImage = new ProductFileImage();
                        productFileImage.FileImageId = temp.Id;
                        productFileImage.ProductId = product.Id;
                        await _productFileImageRepository.CreateAsync(productFileImage);
                    }
                }
            }
            return Ok(result);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            foreach(var product in products)
            {
                foreach(var item in product.ProductFileImages)
                {
                    item.FileImage.FileLocation = "https://localhost:44314" + item.FileImage.FileLocation;
                }
            }
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetById(int id)
        {
            var result = await _productRepository.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet]
        [Route("kerword={keyword}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetByKeyWord(string keyword)
        {
            var result = await _productRepository.GetByNameAsync(keyword);
            return Ok(result);
        }

        [HttpGet]
        [Route("category={keyword}")]
        [AllowAnonymous]
        public async Task<ActionResult> GetByCategory(string keyword)
        {
            var results = await _productRepository.GetByCategoryAsync(keyword);
            return Ok(results);
        }

        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var productFileImages = await _productFileImageRepository.GetByProductId(id);
            if (productFileImages != null && productFileImages.Count() > 0)
            {
                foreach (var productFileImage in productFileImages)
                {
                    await _productFileImageRepository.DeleteAsync(productFileImage.FileImageId, productFileImage.ProductId);
                    var fileImage = await _fileImageRepository.GetById(productFileImage.FileImageId);
                    string fileDes = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                    string[] temp = fileImage.FileLocation.Split("/");
                    fileDes = Path.Combine(fileDes, temp[2].ToString());
                    bool flag = _fileServices.DeleteFileAsync(fileDes);
                    if (flag)
                    {
                        await _fileImageRepository.DeleteAsync(productFileImage.FileImageId);
                    }
                }
                var result = await _productRepository.DeleteAsync(id);
                if (result == null)
                    return NotFound();
                return Ok(result);
            }
            else
                return NotFound();

        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult> Update(int id, [FromForm] ProductRequestVm model)
        {
            var product = await _productRepository.GetByIdAsync(id);
            product.Name = model.Name;
            product.Description = model.Description;
            product.Quantities = model.Quantities;
            product.Price = model.Price;
            product.UpdatedDate = DateTime.Today;
            product.CategoryId = model.CategoryId;
            var result = await _productRepository.UpdateAsync(id, product);
            if ((model.images != null) && (model.images.Count > 0))
            {
                var productFileImages = await _productFileImageRepository.GetByProductId(id);
                if (productFileImages != null && productFileImages.Count() > 0)
                {
                    foreach (var productFileImage in productFileImages)
                    {
                        await _productFileImageRepository.DeleteAsync(productFileImage.FileImageId, productFileImage.ProductId);
                        var fileImage = await _fileImageRepository.GetById(productFileImage.FileImageId);
                        string fileDes = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                        string[] temp = fileImage.FileLocation.Split("/");
                        fileDes = Path.Combine(fileDes, temp[2].ToString());
                        bool flag = _fileServices.DeleteFileAsync(fileDes);
                        if (flag)
                        {
                            await _fileImageRepository.DeleteAsync(productFileImage.FileImageId);
                        }
                    }
                }
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                foreach (IFormFile file in model.images)
                {
                    string fileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, fileName);
                    var kq = _fileServices.UploadFileAsync(filePath, file);
                    if (kq)
                    {
                        FileImage fileImage = new FileImage();
                        fileImage.FileLocation = $"/images/{fileName}";
                        fileImage.CreateDate = DateTime.Today;
                        var temp = await _fileImageRepository.CreateAsync(fileImage);

                        ProductFileImage productFileImage = new ProductFileImage();
                        productFileImage.FileImageId = temp.Id;
                        productFileImage.ProductId = product.Id;
                        await _productFileImageRepository.CreateAsync(productFileImage);
                    }
                }
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("rate")]
        [Authorize]
        public async Task<IActionResult> RateProduct(int productId, int totalStar)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity.FindFirst("sub").Value;
            Rate rate = await _rateRepository.CreateAsync(productId, userId, totalStar);
            return Ok(rate);
        }
    }
}
