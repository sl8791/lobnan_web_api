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
using Innovi.Models.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Innovi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
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

        // POST: api/Categories/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetWithPagination([FromQuery] CategoryFilterDto filter)
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

    }

}
