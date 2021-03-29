using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.Backend.Interfaces;
using Web.Backend.Models;

namespace Web.Backend.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize("Bearer")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<Category>> GetAll()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }
    }
}
