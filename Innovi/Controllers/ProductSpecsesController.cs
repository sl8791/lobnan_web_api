using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Innovi.Data;
using Innovi.Entities;
using Innovi.Services.Interfaces;
using Innovi.Services.Repository;
using Innovi.Models;
using Innovi.Models.Filters;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductSpecsesController : ControllerBase
    {
        private readonly IProductSpecseRepository _productSpecseRepository;

        public ProductSpecsesController(IProductSpecseRepository productSpecseRepository)
        {
            this._productSpecseRepository = productSpecseRepository;
        }

        // GET: api/ProductSpecses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSpecseDto>>> GetProductSpecses()
        {
            try
            {
                var productSpecses = await _productSpecseRepository.GetAllAsync();
                if (productSpecses == null)
                {
                    return NotFound("No ProductSpecses found.");
                }
                return Ok(productSpecses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/ProductSpecses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSpecseDto>> GetProductSpecse(int id)
        {
            try
            {
                var productSpecse = await _productSpecseRepository.GetByIdAsync(id);
                if (productSpecse == null)
                {
                    return NotFound("No ProductSpecses found.");
                }
                return Ok(productSpecse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/ProductSpecses/6/GetByProductId
        [HttpGet("{ProductId}/GetByProductId")]
        public async Task<ActionResult<IEnumerable<ProductSpecseDto>>> GetByCategoryIdAsync(int ProductId)
        {
            try
            {
                var productSpecses = await _productSpecseRepository.GetByProductIdAsync(ProductId);
                if (productSpecses == null)
                {
                    return NotFound("No ProductSpecses found.");
                }
                return Ok(productSpecses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/ProductSpecses/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<ProductSpecseDto>>> GetWithPagination([FromQuery] ProductSpecseFilterDto filter)
        {
            try
            {
                var filteredProducts = await _productSpecseRepository.GetWithPagination(filter);
                if (filteredProducts == null)
                {
                    return NotFound("No ProductSpecses found.");
                }
                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
    }
}
