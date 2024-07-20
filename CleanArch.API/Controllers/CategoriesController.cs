using CleanArch.Application.DTOs;
using CleanArch.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategories()
        {
            var categories = await _categoryService.GetCategories();

            if (categories == null)
            {
                return NotFound("Categories not found!");
            }

            return Ok(categories);
        }

        [HttpGet("{id:int}", Name = "GetCategoryById")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound("Category not found!");
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            if (categoryDto == null)
            {
                return BadRequest("Invalid Data!");
            }

            await _categoryService.Add(categoryDto);

            return new CreatedAtRouteResult("GetCategoryById", new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut]
        public async Task<ActionResult<CategoryDTO>> UpdateCategory(int id, [FromBody] CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest("Invalid Id!");
            }

            if (categoryDto == null)
            {
                return BadRequest("Invalid Data!");
            }

            await _categoryService.Update(categoryDto);

            return Ok(categoryDto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var category = await _categoryService.GetById(id);

            if (category == null)
            {
                return NotFound("Category not found!");
            }

            await _categoryService.Remove(id);

            return Ok();
        }
    }
}
