using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Interfaces;
using Web.ShareModels;

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

        public ProductController(IProductRepository productRepository, IFileImageRepository fileImageRepository, IProductFileImageRepository productFileImageRepository)
        {
            _productRepository = productRepository;
            _fileImageRepository = fileImageRepository;
            _productFileImageRepository = productFileImageRepository;
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
                foreach (IFormFile file in model.images)
                {
                    var temp = await _fileImageRepository.UploadAsync(file);
                    ProductFileImage productFileImage = new ProductFileImage();
                    productFileImage.FileImageId = temp.Id;
                    productFileImage.ProductId = product.Id;
                    await _productFileImageRepository.CreateAsync(productFileImage);
                }
            }
            return Ok(result);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
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
                    await _fileImageRepository.DeleteAsync(productFileImage.FileImageId);
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
                        await _fileImageRepository.DeleteAsync(productFileImage.FileImageId);
                    }
                }
                foreach (IFormFile file in model.images)
                {
                    var temp = await _fileImageRepository.UploadAsync(file);
                    ProductFileImage productFileImage = new ProductFileImage();
                    productFileImage.FileImageId = temp.Id;
                    productFileImage.ProductId = product.Id;
                    await _productFileImageRepository.CreateAsync(productFileImage);
                }
            }
            return Ok(result);

        }
    }
}
