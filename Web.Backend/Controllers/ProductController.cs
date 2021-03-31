using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.Backend.Interfaces;
using Web.Backend.Models;
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
        public ProductController(IProductRepository productRepository, IFileImageRepository fileImageRepository)
        {
            _productRepository = productRepository;
            _fileImageRepository = fileImageRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Create([FromForm] ProductRequestVm model)
        {
            var temp = await _fileImageRepository.UploadAsync(model.image);
            if (temp != null)
            {
                Product product = new Product();
                product.Name = model.Name;
                product.Description = model.Description;
                product.Quantities = model.Quantities;
                product.Price = model.Price;
                //product.FileImageId = temp.Id;
                product.CreatedDate = DateTime.Today;
                product.UpdatedDate = DateTime.Today;
                product.CategoryId = model.CategoryId;
                var result = await _productRepository.CreateAsync(product);
                if (result != null)
                    return Ok(result);
                return BadRequest();
            }
            else
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);
        }
    }
}
