using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Innovi.Data;
using Innovi.Entities;
using Innovi.Models;
using Innovi.Services.Interfaces;
using Castle.Components.DictionaryAdapter.Xml;
using Innovi.Services.Repository;
using Innovi.Models.Filters;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributesController : ControllerBase
    {
        private readonly IAttributeRepository _attributeRepository;

        public AttributesController(IAttributeRepository attributeRepository)
        {
            this._attributeRepository = attributeRepository;
        }

        // GET: api/Attributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> GetAttributes()
        {
            try
            {
                var attributes = await _attributeRepository.GetAllAsync();
                if (attributes == null)
                {
                    return NotFound("No Attributes found.");
                }
                return Ok(attributes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Attributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeDto>> GetAttribute(int id)
        {
            try
            {
                var attribute = await _attributeRepository.GetByIdAsync(id);
                if (attribute == null)
                {
                    return NotFound("No Attributes found.");
                }
                return Ok(attribute);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Attributes/6/GetByMerchantId
        [HttpGet("{MerchantId}/GetByMerchantId")]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> GetByMerchantIdAsync(int MerchantId)
        {
            try
            {
                var attributes = await _attributeRepository.GetByMerchantIdAsync(MerchantId);
                if (attributes == null)
                {
                    return NotFound("No Attributes found.");
                }
                return Ok(attributes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Attributes/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<AttributeDto>>> GetWithPagination(AttributeFilterDto filter)
        {
            try
            {
                var filteredAttributes = await _attributeRepository.GetWithPagination(filter);
                if (filteredAttributes == null)
                {
                    return NotFound("No Attributes found.");
                }
                return Ok(filteredAttributes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
