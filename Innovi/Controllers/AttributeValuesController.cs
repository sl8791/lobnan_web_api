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
    public class AttributeValuesController : ControllerBase
    {
        private readonly IAttributeValueRepository _attributeValueRepository;

        public AttributeValuesController(IAttributeValueRepository attributeValueRepository)
        {
            this._attributeValueRepository = attributeValueRepository;
        }

        // GET: api/AttributeValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AttributeValueDto>>> GetAttributeValues()
        {
            try
            {
                var attributeValues = await _attributeValueRepository.GetAllAsync();
                if (attributeValues == null)
                {
                    return NotFound("No AttributeValues found.");
                }
                return Ok(attributeValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/AttributeValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeValueDto>> GetAttributeValue(int id)
        {
            try
            {
                var attributeValue = await _attributeValueRepository.GetByIdAsync(id);
                if (attributeValue == null)
                {
                    return NotFound("No AttributeValues found.");
                }
                return Ok(attributeValue);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/AttributeValues/6/GetByAttributeIdAsync
        [HttpGet("{AttributeId}/GetByAttributeIdAsync")]
        public async Task<ActionResult<IEnumerable<AttributeValueDto>>> GetByAttributeIdAsync(int AttributeId)
        {
            try
            {
                var attributeValues = await _attributeValueRepository.GetByAttributeIdAsync(AttributeId);
                if (attributeValues == null)
                {
                    return NotFound("No AttributeValues found.");
                }
                return Ok(attributeValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/AttributeValues/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<AttributeValueDto>>> GetWithPagination([FromQuery] AttributeValueFilterDto filter)
        {
            try
            {
                var filteredAttributeValues = await _attributeValueRepository.GetWithPagination(filter);
                if (filteredAttributeValues == null)
                {
                    return NotFound("No AttributeValues found.");
                }
                return Ok(filteredAttributeValues);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
    }
}
