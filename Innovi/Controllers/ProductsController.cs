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
using System.Collections;
using Innovi.Models;
using Innovi.Models.Filters;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            this._productRepository = productRepository;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                if (products == null)
                {
                    return NotFound("No Products found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            try
            {
                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return NotFound("No Products found.");
                }
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Products/6/GetByCategoryIdAsync
        [HttpGet("{CategoryId}/GetByCategoryIdAsync")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByCategoryIdAsync(int CategoryId)
        {
            try
            {
                var products = await _productRepository.GetByCategoryIdAsync(CategoryId);
                if (products == null)
                {
                    return NotFound("No Products found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Products/6/GetByMerchantId
        [HttpGet("{MerchantId}/GetByMerchantId")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByMerchantIdAsync(int MerchantId)
        {
            try
            {
                var products = await _productRepository.GetByMerchantIdAsync(MerchantId);
                if (products == null)
                {
                    return NotFound("No Products found.");
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Products/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetWithPagination(ProductFilterDto filter)
        {
            try
            {
                var filteredProducts = await _productRepository.GetWithPagination(filter);
                if (filteredProducts == null)
                {
                    return NotFound("No Products found.");
                }
                return Ok(filteredProducts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        //TODO

    }
}
