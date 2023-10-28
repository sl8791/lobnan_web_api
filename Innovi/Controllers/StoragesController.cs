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
using Microsoft.AspNetCore.Authorization;

namespace Innovi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoragesController : ControllerBase
    {
        private readonly IStorageRepository _storageRepository;

        public StoragesController(IStorageRepository storageRepository)
        {
            this._storageRepository = storageRepository;
        }

        // GET: api/Storages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StorageDto>>> GetStorages()
        {
            try
            {
                var storages = await _storageRepository.GetAllAsync();
                if (storages == null)
                {
                    return NotFound("No Storages found.");
                }
                return Ok(storages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // GET: api/Storages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageDto>> GetStorage(int id)
        {
            try
            {
                var storage = await _storageRepository.GetByIdAsync(id);
                if (storage == null)
                {
                    return NotFound("No Storages found.");
                }
                return Ok(storage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Storages/6/GetByMerchantId
        [HttpGet("{MerchantId}/GetByMerchantId")]
        public async Task<ActionResult<IEnumerable<StorageDto>>> GetByMerchantIdAsync(int MerchantId)
        {
            try
            {
                var storages = await _storageRepository.GetByMerchantIdAsync(MerchantId);
                if (storages == null)
                {
                    return NotFound("No Storages found.");
                }
                return Ok(storages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Storages/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<StorageDto>>> GetWithPagination([FromQuery] StorageFilterDto filter)
        {
            try
            {
                var filteredStorages = await _storageRepository.GetWithPagination(filter);
                if (filteredStorages == null)
                {
                    return NotFound("No Storages found.");
                }
                return Ok(filteredStorages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
