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
using Microsoft.AspNetCore.Authorization;

namespace Innovi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IProductImageRepository _productImageRepository;

        public ProductImagesController(IProductImageRepository productImageRepository)
        {
            this._productImageRepository = productImageRepository;
        }

        // GET: api/ProductImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductImageDto>>> GetProductImages()
        {
            try
            {
                var productImages = await _productImageRepository.GetAllAsync();
                if (productImages == null)
                {
                    return NotFound("No ProductImages found.");
                }
                return Ok(productImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/ProductImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductImageDto>> GetProductImage(int id)
        {
            try
            {
                var productImage = await _productImageRepository.GetByIdAsync(id);
                if (productImage == null)
                {
                    return NotFound("No ProductImages found.");
                }
                return Ok(productImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/ProductImages/6/GetByProductId
        [HttpGet("{ProductId}/GetByProductId")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByProductIdAsync(int ProductId)
        {
            try
            {
                var productImages = await _productImageRepository.GetByProductIdAsync(ProductId);
                if (productImages == null)
                {
                    return NotFound("No ProductImages found.");
                }
                return Ok(productImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }
}
