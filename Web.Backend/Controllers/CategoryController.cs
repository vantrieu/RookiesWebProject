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
        [AllowAnonymous]
        public async Task<ActionResult<Category>> Get()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet]
        [Route("id={id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            return Ok(category);
        }

        [HttpGet]
        [Route("keyword={name}")]
        [AllowAnonymous]
        public async Task<ActionResult<Category>> GetByName(string name)
        {
            var category = await _categoryRepository.GetByNameAsync(name);
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<Category>> Add(Category model)
        {
            var category = await _categoryRepository.CreateAsync(model);
            return Ok(category);
        }

        [HttpPut]
        [Route("id={id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int id, Category model)
        {
            var category = await _categoryRepository.UpdateAsync(id, model);
            if (category == null)
                return NotFound();
            return Ok(category);
        }


        [HttpDelete]
        [Route("id={id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryRepository.DeleteAsync(id);
            if (category == null)
                return NotFound();
            return Ok(category);
        }
    }
}
