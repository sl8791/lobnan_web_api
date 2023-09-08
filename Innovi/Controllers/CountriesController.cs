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
using Microsoft.AspNetCore.Authorization;

namespace Innovi.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        public CountriesController(ICountryRepository countryRepository)
        {
            this._countryRepository = countryRepository;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetCountries()
        {
            try
            {
                var Countries = await _countryRepository.GetAllAsync();
                if (Countries == null)
                {
                    return NotFound("No Countries found.");
                }
                return Ok(Countries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryDto>> GetCountry(int id)
        {
            try
            {
                var Countrie = await _countryRepository.GetByIdAsync(id);
                if (Countrie == null)
                {
                    return NotFound("No Countries found.");
                }
                return Ok(Countrie);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Countries/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<CountryDto>>> GetWithPagination([FromQuery] CountryFilterDto filter)
        {
            try
            {
                var filteredCountries = await _countryRepository.GetWithPagination(filter);
                if (filteredCountries == null)
                {
                    return NotFound("No Countries found.");
                }
                return Ok(filteredCountries);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
