using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Innovi.Data;
using Innovi.Models;
using System.Diagnostics;
using Innovi.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.Collections;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategorieRepository _categoryRepository;

        public CategoriesController(ICategorieRepository categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllAsync();
                if (categories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            try
            {
                var categories = await _categoryRepository.GetByIdAsync(id);
                if (categories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Categories/6
        [HttpGet("{parentCategoryId}/GetByParentCategoryId")]
        public async Task<ActionResult<IEnumerable>> GetByParentCategoryIdAsync(int parentCategoryId)
        {
            try
            {
                var categories = await _categoryRepository.GetByParentCategoryIdAsync(parentCategoryId);
                if (categories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Categories
        [HttpPost]
        public async Task<ActionResult<CategoryDto>> PostCategory(CategoryDto category)
        {
            try
            {
                if (category == null)
                {
                    return BadRequest("Invalid category data.");
                }
                var createdCategory = await _categoryRepository.CreateAsync(category);
                if (createdCategory == null)
                {
                    return Problem("Failed to create category.");
                }

                return CreatedAtAction("GetCategory", new { id = category.Id }, category);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        // POST: api/Categories/filter
        [HttpPost]
        [Route("filter")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> FilterCategory(CategoryDto filter)
        {
            try
            {
                if (filter.Code.IsNullOrEmpty() && filter.NameEn.IsNullOrEmpty() && filter.NameAr.IsNullOrEmpty())
                {
                    var categories = await _categoryRepository.GetAllAsync();

                    if (categories == null)
                    {
                        return NotFound("No categories found.");
                    }
                    return Ok(categories);
                }
                var filteredCategories = await _categoryRepository.GetAllAsync(filter);
                if (filteredCategories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(filteredCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        // POST: api/Categories/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetWithPagination(CategoryFilterDto filter)
        {
            try
            {
                var filteredCategories = await _categoryRepository.GetWithPagination(filter);
                if (filteredCategories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(filteredCategories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        // PUT: api/Categories/5
        [HttpPut]
        public async Task<IActionResult> PutCategory(CategoryDto category)
        {
            try
            {
                var categories = await _categoryRepository.UpdateAsync(category);
                if (categories == null)
                {
                    return NotFound("No categories found.");
                }
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                var isCategoryRemoved = await _categoryRepository.RemoveAsync(id);

                if (!isCategoryRemoved)
                {
                    return NotFound("Category not found.");
                }

                return NoContent(); // 204 No Content response indicating successful deletion
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        //TODO

    }

}
