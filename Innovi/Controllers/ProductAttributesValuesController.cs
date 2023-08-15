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
using Innovi.Models;
using Microsoft.CodeAnalysis;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductAttributesValuesController : ControllerBase
    {
        private readonly IProductAttributesValueRepository _productAttributesValueRepository;

        public ProductAttributesValuesController(IProductAttributesValueRepository productAttributesValueRepository)
        {
            this._productAttributesValueRepository = productAttributesValueRepository;
        }

        // GET: api/ProductAttributesValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductAttributesValueDto>>> GetProductAttributesValues()
        {
            try
            {
                var productAttributesValues = await _productAttributesValueRepository.GetAllAsync();
                if (productAttributesValues == null)
                {
                    return NotFound("No ProductAttributesValues found.");
                }
                return Ok(productAttributesValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/ProductAttributesValues/5
        [HttpGet("{ProductId}/{AttributeValueId}/{AttributeId}")]
        public async Task<ActionResult<ProductAttributesValueDto>> GetProductAttributesValue(int ProductId, int AttributeValueId, int AttributeId)
        {
            try
            {
                var productAttributesValues = await _productAttributesValueRepository.GetByIdAsync( ProductId, AttributeValueId, AttributeId);
                if (productAttributesValues == null)
                {
                    return NotFound("No ProductAttributesValues found.");
                }
                return Ok(productAttributesValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

      
    }
}
