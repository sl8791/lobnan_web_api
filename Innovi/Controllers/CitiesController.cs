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
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository _cityRepositorycs;

        public CitiesController(ICityRepository cityRepositorycs)
        {
            this._cityRepositorycs = cityRepositorycs;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            try
            {
                var cities = await _cityRepositorycs.GetAllAsync();
                if (cities == null)
                {
                    return NotFound("No Cities found.");
                }
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Cities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            try
            {
                var citie = await _cityRepositorycs.GetByIdAsync(id);
                if (citie == null)
                {
                    return NotFound("No Cities found.");
                }
                return Ok(citie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Cities/6/GetByCountryIdAsync
        [HttpGet("{CountryId}/GetByCountryIdAsync")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetByCountryIdAsync(int CountryId)
        {
            try
            {
                var cities = await _cityRepositorycs.GetByCountryIdAsync(CountryId);
                if (cities == null)
                {
                    return NotFound("No Cities found.");
                }
                return Ok(cities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Cities/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetWithPagination(CityFilterDto filter)
        {
            try
            {
                var filteredCities = await _cityRepositorycs.GetWithPagination(filter);
                if (filteredCities == null)
                {
                    return NotFound("No Cities found.");
                }
                return Ok(filteredCities);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
    }
}
