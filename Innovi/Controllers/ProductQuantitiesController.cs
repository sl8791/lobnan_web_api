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
    [Route("api/[controller]")]
    [ApiController]
    public class ProductQuantitiesController : ControllerBase
    {
        private readonly IProductQuantityRepository _productQuantityRepository;

        public ProductQuantitiesController(IProductQuantityRepository productQuantityRepository)
        {
            this._productQuantityRepository = productQuantityRepository;
        }


        // GET: api/ProductQuantities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductQuantityDto>>> GetProductQuantities()
        {
            try
            {
                var productQuantities = await _productQuantityRepository.GetAllAsync();
                if (productQuantities == null)
                {
                    return NotFound("No ProductQuantities found.");
                }
                return Ok(productQuantities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/ProductQuantities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductQuantityDto>> GetProductQuantity(int id)
        {
            try
            {
                var productQuantitie = await _productQuantityRepository.GetByIdAsync(id);
                if (productQuantitie == null)
                {
                    return NotFound("No ProductQuantities found.");
                }
                return Ok(productQuantitie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/ProductQuantities/6/GetByProductId
        [HttpGet("{ProductId}/GetByProductId")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetByProductIdAsync(int ProductId)
        {
            try
            {
                var productQuantities = await _productQuantityRepository.GetByProductIdAsync(ProductId);
                if (productQuantities == null)
                {
                    return NotFound("No ProductQuantities found.");
                }
                return Ok(productQuantities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
