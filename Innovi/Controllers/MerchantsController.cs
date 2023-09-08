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
    public class MerchantsController : ControllerBase
    {
        private readonly IMerchantRepository _merchantRepository;
        public MerchantsController(IMerchantRepository merchantRepository)
        {
            this._merchantRepository = merchantRepository;
        }
        // GET: api/Merchants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MerchantDto>>> GetMerchants()
        {
            try
            {
                var merchants = await _merchantRepository.GetAllAsync();
                if (merchants == null)
                {
                    return NotFound("No Merchants found.");
                }
                return Ok(merchants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Merchants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MerchantDto>> GetMerchant(int id)
        {
            try
            {
                var merchant = await _merchantRepository.GetByIdAsync(id);
                if (merchant == null)
                {
                    return NotFound("No Merchants found.");
                }
                return Ok(merchant);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Merchants/6/GetByCountryId
        [HttpGet("{CountryId}/GetByCountryId")]
        public async Task<ActionResult<IEnumerable<MerchantDto>>> GetByCountryIdAsync(int CountryId)
        {
            try
            {
                var merchants = await _merchantRepository.GetByCountryIdAsync(CountryId);
                if (merchants == null)
                {
                    return NotFound("No Merchants found.");
                }
                return Ok(merchants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Merchants/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<MerchantDto>>> GetWithPagination([FromQuery] MerchantFilterDto filter)
        {
            try
            {
                var filteredMerchants = await _merchantRepository.GetWithPagination(filter);
                if (filteredMerchants == null)
                {
                    return NotFound("No Merchants found.");
                }
                return Ok(filteredMerchants);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }


    }
}
