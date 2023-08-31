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
using Innovi.Services.Repository;
using Innovi.Models.Filters;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturersController(IManufacturerRepository manufacturerRepository)
        {
            this._manufacturerRepository = manufacturerRepository;
        }

        // GET: api/Manufacturers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ManufacturerDto>>> GetManufacturers()
        {
            try
            {
                var manufacturers = await _manufacturerRepository.GetAllAsync();
                if (manufacturers == null)
                {
                    return NotFound("No Manufacturers found.");
                }
                return Ok(manufacturers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Manufacturers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ManufacturerDto>> GetManufacturer(int id)
        {
            try
            {
                var manufacturer = await _manufacturerRepository.GetByIdAsync(id);
                if (manufacturer == null)
                {
                    return NotFound("No Manufacturers found.");
                }
                return Ok(manufacturer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Manufacturers/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<ManufacturerDto>>> GetWithPagination([FromQuery] ManufacturerFilterDto filter)
        {
            try
            {
                var filteredManufacturers = await _manufacturerRepository.GetWithPagination(filter);
                if (filteredManufacturers == null)
                {
                    return NotFound("No Manufacturers found.");
                }
                return Ok(filteredManufacturers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
