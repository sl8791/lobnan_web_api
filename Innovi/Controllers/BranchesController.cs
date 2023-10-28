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
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchRepository _branchRepository;

        public BranchesController(IBranchRepository branchRepository)
        {
            this._branchRepository = branchRepository;
        }

        // GET: api/Branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetBranches()
        {
            try
            {
                var branches = await _branchRepository.GetAllAsync();
                if (branches == null)
                {
                    return NotFound("No Branches found.");
                }
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetBranch(int id)
        {
            try
            {
                var branche = await _branchRepository.GetByIdAsync(id);
                if (branche == null)
                {
                    return NotFound("No Branches found.");
                }
                return Ok(branche);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Branches/6/GetByCityId
        [HttpGet("{CityId}/GetByCityId")]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetByCityIdAsync(int CityId)
        {
            try
            {
                var branches = await _branchRepository.GetByCityIdAsync(CityId);
                if (branches == null)
                {
                    return NotFound("No Branches found.");
                }
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // GET: api/Branches/6/GetByMerchantId
        [HttpGet("{MerchantId}/GetByMerchantId")]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetByMerchantIdAsync(int MerchantId)
        {
            try
            {
                var branches = await _branchRepository.GetByMerchantIdAsync(MerchantId);
                if (branches == null)
                {
                    return NotFound("No Branches found.");
                }
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        // POST: api/Branches/Pagination
        [HttpPost]
        [Route("Pagination")]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetWithPagination([FromQuery] BranchFilterDto filter)
        {
            try
            {
                var filteredBranches = await _branchRepository.GetWithPagination(filter);
                if (filteredBranches == null)
                {
                    return NotFound("No Branches found.");
                }
                return Ok(filteredBranches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

    }
}
