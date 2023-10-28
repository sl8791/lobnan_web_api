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
using Innovi.Models.Filters;
using Microsoft.AspNetCore.Authorization;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernoratesController : ControllerBase
    {
        private readonly IGovernorateRepository _governorateRepository;

        public GovernoratesController(IGovernorateRepository governorateRepository)
        {
            this._governorateRepository = governorateRepository;
        }

        // GET: api/Governorates
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GovernorateDto>>> GetGovernorates()
        {
            try
            {
                var governorates = await _governorateRepository.GetAllAsync();
                if (governorates == null)
                {
                    return NotFound("No Governorates found.");
                }
                return Ok(governorates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Governorates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GovernorateDto>> GetGovernorate(int id)
        {
            try
            {
                var governorate = await _governorateRepository.GetByIdAsync(id);
                if (governorate == null)
                {
                    return NotFound("No Governorates found.");
                }
                return Ok(governorate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Governorates/6/GetByCountryIdAsync
        [HttpGet("{CountryId}/GetByCountryIdAsync")]
        public async Task<ActionResult<IEnumerable<GovernorateDto>>> GetByCountryIdAsync(int CountryId)
        {
            try
            {
                var governorates = await _governorateRepository.GetByCountryIdAsync(CountryId);
                if (governorates == null)
                {
                    return NotFound("No Governorates found.");
                }
                return Ok(governorates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Governorates/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<GovernorateDto>>> GetWithPagination([FromQuery] GovernorateFilterDto filter)
        {
            try
            {
                var filteredGovernorates = await _governorateRepository.GetWithPagination(filter);
                if (filteredGovernorates == null)
                {
                    return NotFound("No Governorates found.");
                }
                return Ok(filteredGovernorates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
